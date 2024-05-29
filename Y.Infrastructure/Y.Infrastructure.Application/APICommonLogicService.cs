using Microsoft.AspNetCore.Http;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Entities.RequestModel;
using Y.Infrastructure.Entities.ViewModel;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IMerchants;
using Y.Packet.Services.IPromotions;
using Y.Packet.Services.IVips;

namespace Y.Infrastructure.Application
{
    public class APICommonLogicService : IAPICommonLogicService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersService _usersService;
        private readonly IUsersLoginLogService _usersLoginLogService;
        private readonly IBaseHandlerService _baseHandler;
        private readonly IUsersSessionService _usersSessionService;
        private readonly IMerchantService _merchantService;
        private readonly ISectionKeyService _sectionKeyService;
        private readonly INoticeAreaService _noticeAreaService;
        private readonly IPromotionsConfigService _promotionsConfigService;
        private readonly IUsersFundsService _usersFundsService;
        private readonly IGameMerchantService _gameMerchantService;
        private readonly IGameUsersService _gameUsersService;
        private readonly IGameInfoService _gameInfoService;
        private readonly IInfrastructureGamesService _infrastructureGamesService;
        private readonly IVipGroupsService _vipGroupsService;
        private readonly IWashOrderDetailService _washOrderDetailService;
        public APICommonLogicService(IHttpContextAccessor httpContextAccessor, IUsersService usersService, IUsersLoginLogService usersLoginLogService, IBaseHandlerService baseHandler, IUsersSessionService usersSessionService, IMerchantService merchantService, ISectionKeyService sectionKeyService, INoticeAreaService noticeAreaService, IUsersFundsService usersFundsService, IGameMerchantService gameMerchantService, IGameUsersService gameUsersService, IGameInfoService gameInfoService, IInfrastructureGamesService infrastructureGamesService, IVipGroupsService vipGroupsService, IWashOrderDetailService washOrderDetailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _usersService = usersService;
            _usersLoginLogService = usersLoginLogService;
            _baseHandler = baseHandler;
            _usersSessionService = usersSessionService;
            _merchantService = merchantService;
            _sectionKeyService = sectionKeyService;
            _noticeAreaService = noticeAreaService;
            _promotionsConfigService = (IPromotionsConfigService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IPromotionsConfigService));
            _usersFundsService = usersFundsService;
            _gameMerchantService = gameMerchantService;
            _gameUsersService = gameUsersService;
            _gameInfoService = gameInfoService;
            _infrastructureGamesService = infrastructureGamesService;
            _vipGroupsService = vipGroupsService;
            _washOrderDetailService = washOrderDetailService;
        }


        public async Task<(bool, string)> SignUp(UserRegisterModel req)
        {
            // TODO 防止恶意注册  加黑IP
            // TODO 未来放到Merchant缓存中
            req.MerchantId = _baseHandler.MerchantId;
            req.DefaultGroupId = (await _vipGroupsService.GetDefaultGroupIdAsync(_baseHandler.MerchantId)).Item3;

            var mch = (await _merchantService.GetAsync(_baseHandler.MerchantId)).Item2;
            // 获取到站点的注册配置，进行验证
            if (mch.MerchantSignupConfig.EnableValidCode && req.ValidCode.IsNullOrEmpty())
                return (false, "请填写验证码");
            else if (!req.ValidCode.IsNullOrEmpty())
            {
                // TODO redis缓存根据IP取出来对应的验证码进行核对

            }

            if (mch.MerchantSignupConfig.EnableCode)
                if (req.Code.IsNullOrEmpty() && req.RegisterUrl.IsNullOrEmpty())
                    return (false, "已经开启代理模式,请输入代理码");
            if (mch.MerchantSignupConfig.EnableFPasw && req.FPasw.IsNullOrEmpty()) return (false, "出款密码设置错误");
            if (mch.MerchantSignupConfig.EnablePhone && req.Mobile.IsNullOrEmpty()) return (false, "请填写手机号码");
            if (mch.MerchantSignupConfig.EnableIdName && req.IdName.IsNullOrEmpty()) return (false, "请填写真实姓名");

            var rt = await _usersService.InsertAsync(req);
            return (rt.Item1, rt.Item2);
        }


        public async Task<(bool, object)> GetCsConfig()
        {
            if (_baseHandler.MerchantId == 0) return (false, "商户不存在");
            var mch = (await _merchantService.GetAsync(_baseHandler.MerchantId)).Item2;
            var host = _baseHandler.Host;

            return (true, new
            {
                Name = mch.Name,
                CsConfig = mch.MerchantCustomerConfig,
                IsAgent = true,
            });
        }
        public async Task<(bool, object)> GetCsConfigForPcAsync()
        {

            if (_baseHandler.MerchantId == 0) return (false, "商户不存在");
            var mch = (await _merchantService.GetAsync(_baseHandler.MerchantId)).Item2;
            string qrImg = QRCoderHelper.GetQRCodeBase64String(mch.MerchantCustomerConfig.DownloadQRCode);
            return (true, new
            {
                Name = mch.Name,
                CsConfig = new
                {
                    PcLogo = mch.MerchantCustomerConfig.PcLogo,
                    ServiceLink = mch.MerchantCustomerConfig.ServiceLink,
                    DownloadQRCode = $"data:image/png;base64,{qrImg}",
                    H5SiteUrl = mch.MerchantCustomerConfig.H5SiteUrl,
                    EnabledAgentPattern = mch.MerchantCustomerConfig.EnabledAgentPattern,
                }
            });
        }

        public async Task<(bool, string, UserFundsViewModel)> GetMemberBalanceAsync()
        {
            var data = new UserFundsViewModel();
            if (_baseHandler.MemberId == 0) return (false, "请登录后进行操作", null);
            data.AvailableAmount = await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.LockedAmount = await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.TotalAmount = data.AvailableAmount + data.LockedAmount;
            //data.IsAgent = await _usersService.IsAgentAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            return (true, "", data);
        }

        public async Task<(bool, string, UserFundsViewModelV2)> GetMemberBalanceV2Async()
        {
            var data = new UserFundsViewModelV2();
            if (_baseHandler.MemberId == 0) return (false, "请登录后进行操作", null);
            data.AvailableAmount = await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.LockedAmount = await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.TotalAmount = data.AvailableAmount + data.LockedAmount;
            var gameUsers = (await _gameUsersService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToDictionary(t => t.TypeStr, t => t.Balance);
            data.GamesAmount = gameUsers.Sum(t => t.Value);
            var userInfo = await _usersService.GetUserById(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.Level = userInfo.Item3.GroupId;
            data.LevelImg = null;
            //data.IsAgent = await _usersService.IsAgentAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.IsAgent = userInfo.Item3.Type == Users.UserType.Agent;
            // TODO 添加等级的图片
            return (true, "", data);
        }

        public async Task<(bool, string, UserFundsViewModelV3)> GetMemberBalanceV3Async()
        {
            var data = new UserFundsViewModelV3();
            if (_baseHandler.MemberId == 0) return (false, "请登录后进行操作", null);
            var funds = await _usersFundsService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.AvailableAmount = funds.TotalBetFunds - funds.LockFunds;  // await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.LockedAmount = funds.LockFunds; // await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.TotalAmount = funds.TotalBetFunds;
            var userInfo = await _usersService.GetUserById(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.Level = userInfo.Item3.GroupId;
            data.IsAgent = userInfo.Item3.Type == Users.UserType.Agent;

            // 下一个等级的信息
            var nextLevel = await _vipGroupsService.GetVipNextLevelAsync(_baseHandler.MerchantId, userInfo.Item3.GroupId);
            // 获取当前用户的存款/打码量
            data.DiffDeposit = nextLevel.GroupSettingModel.AccumulatedRechargeAmount > funds.TotalRechargedFunds ? 0 : (nextLevel.GroupSettingModel.AccumulatedRechargeAmount - funds.TotalRechargedFunds);
            data.DiffWash = nextLevel.GroupSettingModel.AccumulatedEffectiveAmount > funds.TotalRechargedFunds ? 0 : (nextLevel.GroupSettingModel.AccumulatedRechargeAmount - funds.TotalRechargedFunds);
            data.Day = DateTime.Now.ToUtc8DateTime().GetDiffDay(userInfo.Item3.CreateTime);
            //data.IsAgent = await _usersService.IsAgentAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            // TODO 添加等级的图片
            return (true, "", data);
        }
        #region W27 INDEX PAGE
        public async Task<(bool, string, W27UserInfo)> GetUserInfoForW27Async()
        {
            //Console.WriteLine(_baseHandler.MemberId);
            //Console.WriteLine(_baseHandler.MerchantId);
            //Console.WriteLine(_baseHandler.Member.VipId);

            var data = new W27UserInfo();
            if (_baseHandler.MemberId == 0) return (false, "请登录后进行操作", null);
            var usr = (await _usersService.GetUserById(_baseHandler.MerchantId, _baseHandler.MemberId)).entity;
            data.Name = usr.AccountName;
            data.Photo = string.Empty;
            data.LastLoginIpAddr = "北京市";
            data.TotalBet = 123458;
            return (true, null, data);
        }

        public async Task<(bool, string, W27Vips)> GetVipsForW27Async()
        {
            var data = new W27Vips();
            var vipList = await _vipGroupsService.GetListAsync(_baseHandler.MerchantId);
            var currentLevel = vipList.Where(t => t.Id == _baseHandler.Member.VipId).Single();
            var nextLevel = vipList.Where(t => t.SortNo == (currentLevel.SortNo + 1)).Single();

            data.CurrentLevel = currentLevel.Id;
            data.NextLevel = nextLevel.Id;
            data.CurrentLevelImage = currentLevel.Img;
            data.NextLevelImage = nextLevel.Img;

            return (true, null, data);
        }

        public async Task<(bool, string, W27FundsInfo)> GetFundsForW27Async()
        {
            var data = new W27FundsInfo();
            var gameUsers = (await _gameUsersService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToDictionary(t => t.TypeStr, t => t.Balance);
            data.Valid = await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.Lock = await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            data.Center = data.Valid + data.Center;
            data.Games = gameUsers.Sum(t => t.Value);
            return (true, null, data);
        }

        public async Task<(bool, string, List<W27WithdrawalsRange>)> GetWithdrawalsRangeForW27Async()
        {
            var data = new List<W27WithdrawalsRange>();
            for (int i = 0; i <= 3; i++)
            {
                var d = new W27WithdrawalsRange();
                d.SortNo = i;
                d.Amount = (new Random()).Next(100, 999);
                d.Name = $"adbd{(new Random()).Next(100, 999)}";
                d.Photo = null;
                data.Add(d);
            }
            return (true, null, data);
        }


        #endregion


        public async Task<(bool, string, UserLoginRt)> LogIn(UserSignInModel req)
        {
            // TODO 防止恶意撞库   加黑IP  同一IP五分钟内重复注册的，禁用
            req.MerchantId = _baseHandler.MerchantId;
            // TODO  写入登录日志  IP
            var rt = await _usersService.SignInAsync(req);
            await _usersLoginLogService.InsertAsync(rt.Item1, req.MerchantId, 10006, req.Name, "ip", req.ToJson());
            return rt;
        }

        public async Task<SiteMainPageDataBaseViewModel> MainPageDataLoadAsync()
        {
            // banner,message info,promo activity,pop up ads
            var banners = await _sectionKeyService.GetByKeyAsync(_baseHandler.MerchantId, "BANNER");
            var messages = await _noticeAreaService.GetPageListAsync(new NoticeAreaListQuery() { MerchantId = _baseHandler.MerchantId, Limit = 3 });
            var promos = await _promotionsConfigService.GetHomePageDisplayActivityAsync(_baseHandler.MerchantId);  // get hot activity 增加字段
            var popupadds = await _noticeAreaService.GetHomePageDisplayAsync(_baseHandler.MerchantId);

            var vm = new SiteMainPageDataBaseViewModel();
            if (messages.Item1.IsNotEqualNull())
                vm.Message = messages.Item1.Select(t => new MessageForMainPage
                {
                    Title = t.Title,
                    Content = t.Description
                });
            vm.Promo = promos.Select(t => new PopUpForMainPage()
            {
                Img = t.Value,
                Link = t.Key.ToString()
            });
            if (popupadds.IsNotEqualNull())
                vm.PopUpAds = popupadds.Select(t => new MessageForMainPage()
                {
                    Title = t.Title,
                    Content = t.Description
                });
            if (banners.IsNotEqualNull())
                vm.Banner = banners.Select(t => new BannerViewModel()
                {
                    Title = t.Tcontent,
                    Link = t.Id.ToString(),
                    Img = _baseHandler.Device.IsMobile() ? t.H5ImgUrl : t.PcImgUrl,
                });
            return vm;
        }

        public async Task<SiteMainPageMessageViewModel> MainPageMessageDataLoadAsync()
        {
            var messages = await _noticeAreaService.GetPageListAsync(new NoticeAreaListQuery() { MerchantId = _baseHandler.MerchantId, Limit = 3 });
            var popupadds = await _noticeAreaService.GetHomePageDisplayAsync(_baseHandler.MerchantId);
            var vm = new SiteMainPageMessageViewModel();
            if (messages.Item1.IsNotEqualNull())
                vm.Message = messages.Item1.Select(t => new MessageForMainPage
                {
                    Title = t.Title,
                    Content = t.Description
                });

            if (popupadds.IsNotEqualNull())
                vm.PopUpAds = popupadds.Select(t => new MessageForMainPage()
                {
                    Title = t.Title,
                    Content = t.Description
                });

            return vm;
        }


        public async Task<(bool, string)> SignOut(int memeberid)
        {
            return await _usersSessionService.DeleteAsync(_baseHandler.MemberId);
        }



        #region Game


        public async Task<(bool, string)> GameLogIn(string type, string code)
        {
            if (_baseHandler.Member.IsEqualNull()) return (false, "请登陆后进行操作");
            if (_baseHandler.Member.Name == null) return (false, "请登陆后进行操作");
            // 判断游戏是否存在
            var t = type.ToEnum<GameType>();
            if (t == null) return (false, "游戏不存在");
            GameType gType = type.ToEnum<GameType>().Value;

            // 判断当前游戏状态  游戏列表里面判断状态
            var gStatus = await _gameInfoService.GetGameStatusAsync(gType);
            if (!gStatus.Item1) return gStatus;

            // 判断站点游戏是否开启
            if (!await _gameMerchantService.IsEnabledGameAsync(_baseHandler.MerchantId, gType)) return (false, "站点未开启该游戏");

            var gameConfig = await _gameInfoService.GetGameConfig(gType);
            string playerName = string.Empty;
            // 1. 判断用户是否注册过，如果注册过则获取游戏名称，获取游戏名称和相关参数信息进行登录
            Dictionary<string, string> args = new Dictionary<string, string>();
            args["device"] = _baseHandler.Device.ToString();
            args["ip"] = _baseHandler.IP;
            args["code"] = code;
            var existGameUsers = await _gameUsersService.ExistAsync(_baseHandler.MemberId, gType);
            if (existGameUsers.exist)
                return await _infrastructureGamesService.LoginAsync(_baseHandler.MerchantId, _baseHandler.MemberId, existGameUsers.gameUserName, gType, args, gameConfig);
            // 2. 未注册则进行注册
            var regisResult = await _infrastructureGamesService.RegisterAsync(_baseHandler.MerchantId, _baseHandler.MemberId, _baseHandler.Member.Name, gType, gameConfig);
            if (!regisResult.Item1) return regisResult;
            playerName = await _gameUsersService.GetPlayerNameAsync(_baseHandler.MemberId, gType);
            // 3. 返回登录信息
            return await _infrastructureGamesService.LoginAsync(_baseHandler.MerchantId, _baseHandler.MemberId, playerName, gType, args, gameConfig);
        }

        public async Task<WalletViewModelV2> GameUserBalanceV2Async()
        {
            var viewModel = new WalletViewModelV2();
            viewModel.LockdownWallet = (await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToCnCurrencyString();
            viewModel.CenterWallet = (await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToCnCurrencyString();
            var gameMerchants = (await _gameMerchantService.GetListAsync(new MerchantGameListQuery()
            {
                MerchantId = _baseHandler.MerchantId
            })).Item1;
            var gameUsers = (await _gameUsersService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToDictionary(t => t.TypeStr, t => t.Balance);

            List<GameWalletCate> GameCate = new List<GameWalletCate>();
            foreach (var g in gameMerchants)
            {
                var d = new GameWalletCate();
                var type = g.Type.TransToGameCate();
                GameCate = BuildGameWalletCate(GameCate, d, type, gameUsers, g);
            }
            viewModel.GameCate = GameCate;
            return viewModel;
        }


        public async Task<List<W27GameTypeWalletViewModel>> GetW27GamesWalletAsync()
        {
            var d = new List<W27GameTypeWalletViewModel>();
            var gameMerchants = (await _gameMerchantService.GetListAsync(new MerchantGameListQuery()
            {
                MerchantId = _baseHandler.MerchantId
            })).Item1;
            var gameUsers = (await _gameUsersService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToDictionary(t => t.TypeStr, t => t.Balance);
            foreach (var g in GameCategory.Chess.GetEnumTypeDic())
            {
                GameCategory gcate = g.Value.ToEnum<GameCategory>().Value;
                if (gcate == GameCategory.NotSet) continue;
                var gw = new W27GameTypeWalletViewModel()
                {
                    Cate = gcate.GetDescription(),
                    Type = gcate.ToString(),
                    Icon = string.Empty,
                    Balance = 0,
                    Games = gameMerchants.Where(t => t.Type.TransToGameCate() == gcate).Select(t => new W27GameDWalletViewModel
                    {
                        Name = t.Type.GetDescription(),
                        Type = t.TypeStr,
                        Balance = gameUsers.GetByKey(t.TypeStr),
                        Icon = string.Empty,
                    }).ToList()
                };
                d.Add(gw);
            }

            return d;
        }


        private List<GameWalletCate> BuildGameWalletCate(List<GameWalletCate> GameCate, GameWalletCate d, GameCategory type, Dictionary<string, decimal> gameUsers, GameMerchant g)
        {
            if (GameCate.Exists(t => t.Type == type.ToString()))
            {
                d = GameCate.Where(t => t.Type == type.ToString()).Single();
                d.Balance += gameUsers.GetByKey(g.TypeStr);
            }
            else
            {
                d.Balance = gameUsers.GetByKey(g.TypeStr);
                d.Name = type.GetDescription();
                d.Type = type.ToString();
                GameCate.Add(d);
            }
            d.GamesWallet.Add(new GameWalletV2()
            {
                Balance = gameUsers.GetByKey(g.TypeStr),
                Type = g.TypeStr,
                Name = g.TypeDesc,
                CateType = type.ToString(),
            });
            return GameCate;
        }

        public async Task<WalletViewModel> GameUserBalanceAsync()
        {
            var viewModel = new WalletViewModel();
            viewModel.LockdownWallet = (await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToCnCurrencyString();
            viewModel.CenterWallet = (await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToCnCurrencyString();
            var gameMerchants = (await _gameMerchantService.GetListAsync(new MerchantGameListQuery()
            {
                MerchantId = _baseHandler.MerchantId,
                Enabled = true,
                SysEnabled = true

            })).Item1;
            var gameUsers = (await _gameUsersService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId)).ToDictionary(t => t.TypeStr, t => t.Balance);

            viewModel.GamesWallet = gameMerchants.Select(t => new GameWallet()
            {
                Balance = gameUsers.GetByKey(t.TypeStr),
                Type = t.TypeStr,
                Name = t.TypeDesc
            });
            return viewModel;
        }

        public async Task<(bool sucess, string msg, string transId)> Transfer(TransferReq req)
        {
            GameType gType = req.Type;
            var subType = req.Type.ToString().ToEnum<FundLogType_Games>();
            if (subType == null || !subType.HasValue) return (false, "站点未开启该游戏", null);
            if (req.Money == 0) return (false, "转账金额错误", null);
            decimal money = Math.Abs(req.Money);
            var playerName = await _gameUsersService.GetPlayerNameAsync(_baseHandler.MemberId, gType);

            var gameConfig = await _gameInfoService.GetGameConfig(gType);
            if (string.IsNullOrEmpty(playerName))
            {
                //return (false, "请先进入游戏后再进行转账", null);
                var regisResult = await _infrastructureGamesService.RegisterAsync(_baseHandler.MerchantId, _baseHandler.MemberId, _baseHandler.Member.Name, gType, gameConfig);

                if (!regisResult.Item1) return (regisResult.Item1, regisResult.Item2, string.Empty);
                playerName = await _gameUsersService.GetPlayerNameAsync(_baseHandler.MemberId, gType);
            }

            // 判断游戏状态
            var gStatus = await _gameInfoService.GetGameStatusAsync(gType);
            if (!gStatus.Item1) return (false, gStatus.errormsg, null);

            // 判断站点游戏是否开启
            if (!await _gameMerchantService.IsEnabledGameAsync(_baseHandler.MerchantId, gType)) return (false, "站点未开启该游戏", null);


            // 1. 判断用户状态是否可以转账


            // 2. 判断站点金额是否足够
            //if (_baseHandler.Merchant.GameCredit < money) return (false, "授信额度不足,请联系客服");


            // 3. 判断用户是否有足够金额
            var userFunds = await _usersFundsService.GetUserAvailableFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            if (req.TransType == TransType.In && money > userFunds) return (false, $"资金不足", null);

            var apiTrans = await _infrastructureGamesService.TransferAsync(_baseHandler.MerchantId, _baseHandler.MemberId, playerName, gType, 0, money, req.TransType, gameConfig);

            if (apiTrans.Item4 == null) return (apiTrans.Item1, apiTrans.Item2, null);

            // 4. 写入用户转账操作日志   转入成功，直接写入，转入失败，锁定金额
            // TODO 增加事件总线
            string desc = $"{gType.GetDescription()}  {req.TransType.GetDescription()}  来源ID:{apiTrans.Item3}";
            //var moneylog = await _usersFundsLogService.AddFundsLogAsync(_baseHandler.MemberId, _baseHandler.MerchantId, money, apiTrans.Item3, FundLogType.Games, (byte)subType.Value.GetEnumValue(), req.TransType, desc, "", apiTrans.Item1);

            if (apiTrans.Item4 != TransferStatus.Failed) // 直接失败的转账不记录日志
            {
                decimal lockAmount = 0;
                if (apiTrans.Item4 == TransferStatus.None) // 游戏锁定状态  并且  转账状态返回的是失败
                    lockAmount = money;

                bool sucess = apiTrans.Item4 == TransferStatus.Sucess;

                if (req.TransType == TransType.In)
                    await _usersFundsService.TransInGameAsync(_baseHandler.MerchantId, _baseHandler.MemberId, apiTrans.Item3.ToInt32().Value, gType, gType.TransToFundLogType(), money, lockAmount, sucess);
                else
                    await _usersFundsService.TransOutGameAsync(_baseHandler.MerchantId, _baseHandler.MemberId, apiTrans.Item3.ToInt32().Value, gType, gType.TransToFundLogType(), money, lockAmount, sucess);
            }


            return (apiTrans.sucess, apiTrans.msg, apiTrans.transId);
        }

        #endregion

        public async Task<object> GetVips()
        {
            if (_baseHandler.Member.IsEqualNull()) return null;
            var vipList = await _vipGroupsService.GetListAsync(_baseHandler.MerchantId);
            List<VipGroupVM> vipL = new List<VipGroupVM>();
            foreach (var l in vipList)
            {
                vipL.Add(new VipGroupVM()
                {
                    Id = l.Id,
                    Name = l.GroupName,
                    SortNo = l.SortNo,
                    RechargeAmount = l.GroupSettingModel.AccumulatedRechargeAmount,
                    EffectiveAmount = l.GroupSettingModel.AccumulatedEffectiveAmount,
                    ProAmount = l.GroupSettingModel.ProAmount,
                    MonthSalary = l.GroupSettingModel.MonthSalary,
                    WeeklySalary = l.GroupSettingModel.WeeklySalary,
                    WithdrawalsCount = l.GroupSettingModel.WithdrawalsCount,
                    WithdrawalTotalAccount = l.GroupSettingModel.WithdrawalDailyTotalAmount,
                    VipBonus = l.GroupSettingModel.VipBonus,
                    VipWashMultiple = l.GroupSettingModel.VipWashMultiple,
                });
            }

            // 获取用户当前存款，用户当前打码
            var funds = await _usersFundsService.GetAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            var curEffectiveAmount = await _washOrderDetailService.GetTotalWashAmountAsync(_baseHandler.MemberId);

            var currentLevel = vipList.Where(t => t.Id == _baseHandler.Member.VipId).Single();
            var nextLevel = vipList.Where(t => t.SortNo == (currentLevel.SortNo + 1)).SingleOrDefault();
            if (nextLevel == null)
                nextLevel = currentLevel;


            decimal per = 0;
            if (curEffectiveAmount > currentLevel.GroupSettingModel.AccumulatedEffectiveAmount && funds.TotalRechargedFunds > currentLevel.GroupSettingModel.AccumulatedRechargeAmount)
            {
                // 升级当前用户等级
            }
            else
            {
                if (curEffectiveAmount > currentLevel.GroupSettingModel.AccumulatedEffectiveAmount) per = funds.TotalRechargedFunds / currentLevel.GroupSettingModel.AccumulatedRechargeAmount;
                else per = curEffectiveAmount / currentLevel.GroupSettingModel.AccumulatedEffectiveAmount;
            }

            var d = new
            {
                VipId = _baseHandler.Member.VipId,
                Vips = vipL.OrderBy(t => t.SortNo),
                CurrentInfo = new
                {
                    CurrentVipId = _baseHandler.Member.VipId,
                    NextLevelVipId = nextLevel.SortNo,
                    CurrentDeposit = funds.TotalRechargedFunds,
                    CurrentBetEffective = curEffectiveAmount,
                    Per = per,
                    CurrentEffectiveAmount = currentLevel.GroupSettingModel.AccumulatedEffectiveAmount,
                    NextLevelEffectiveAmount = nextLevel.GroupSettingModel.AccumulatedEffectiveAmount
                }
            };
            return d;
        }

    }
}
