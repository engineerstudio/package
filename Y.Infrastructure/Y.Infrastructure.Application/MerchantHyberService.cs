//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Y.Infrastructure.Library.Core.AuthController.IService;
//using Y.Infrastructure.Library.Core.Extensions;
//using Y.Infrastructure.Library.Core.Helper;
//using Y.Infrastructure.Library.Core.LogsCoreController.IService;
//using Y.Infrastructure.Library.Core.YEntity;
//using Y.Infrastructure.IApplication;
//using Y.Packet.Entities.Games;
//using Y.Packet.Entities.Members;
//using Y.Packet.Entities.Merchants;
//using Y.Packet.Entities.Merchants.ViewModels;
//using Y.Packet.Entities.Promotions;
//using Y.Packet.Entities.Vips;
//using Y.Packet.Repositories.IGames;
//using Y.Packet.Repositories.IMembers;
//using Y.Packet.Repositories.IMerchants;
//using Y.Packet.Repositories.IPay;
//using Y.Packet.Repositories.IPromotions;
//using Y.Packet.Repositories.IVips;
//using Y.Packet.Services.IGames;
//using Y.Packet.Services.IMembers;
//using Y.Packet.Services.IMerchants;
//using Y.Packet.Services.IPay;
//using Y.Packet.Services.IPromotions;
//using Y.Packet.Services.IVips;
//using static Y.Packet.Entities.Merchants.SectionKey;
//using static Y.Packet.Entities.Promotions.PromotionsConfig;

//namespace Y.Infrastructure.Application
//{
//    public class MerchantHyberService : IMerchantHyberService
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        private readonly IMerchantService _merchantService;
//        private ITempletKeyService _templetKeyService;
//        private ITempletDetailService _templetDetailService;
//        private ITempletGameService _templetGameService;
//        private readonly ISectionKeyService _sectionKeyService;
//        private readonly ISectionDetailService _sectionDetailService;
//        private ISysAccountService _sysAccountService;
//        private IVipGroupsService _vipGroupsService;
//        private IUsersService _usersService;

//        public MerchantHyberService(IHttpContextAccessor httpContextAccessor, IMerchantService merchantService, ISectionKeyService sectionKeyService, ISectionDetailService sectionDetailService)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _merchantService = merchantService;
//            _sectionKeyService = sectionKeyService;
//            _sectionDetailService = sectionDetailService;

//        }

//        public async Task<bool> CreateMerchantSiteAsync(string merchName, string pcTemplet, string h5Templet)
//        {
//            _templetKeyService = (ITempletKeyService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletKeyService));
//            _templetDetailService = (ITempletDetailService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletDetailService));
//            _templetGameService = (ITempletGameService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletGameService));
//            _sysAccountService = (ISysAccountService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ISysAccountService));
//            _vipGroupsService = (IVipGroupsService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IVipGroupsService));
//            _usersService = (IUsersService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IUsersService));

//            // 0. 插入数据
//            var insertMerchant_rt = await _merchantService.AddAsync(merchName, pcTemplet, h5Templet);
//            if (!insertMerchant_rt.Item1) return false;
//            int merchantId = insertMerchant_rt.Item3;

//            // 1. 获取所有key
//            var templetkeylist = (await _templetKeyService.GetByTempletAsync()).Item2;

//            // 1. 获取h5模板的游戏配置数据
//            var templetgamelist = (await _templetGameService.GetListAsync(h5Templet));

//            // 2. 获取PC模板数据
//            var templetdetaillist = (await _templetDetailService.GetListAsync(pcTemplet)).Item2;

//            //List<SectionKey> lsSectionKey = new List<SectionKey>();
//            // 3. 同步数据到Merchant表
//            // 在这里同步一条key，那么同步一条details
//            foreach (var key in templetkeylist)
//            {
//                var secKey = new SectionKey()
//                {
//                    MerchantId = merchantId,
//                    Type = SectionKey.KeyType.PC,
//                    DetailType = key.DetailType.ToString().ToEnum<KeyDetailType>().Value,
//                    SKey = key.SKey,
//                    Description = key.Description
//                };
//                //lsSectionKey.Add(secKey);
//                // 添加后获取到Id，然后根据这个Id存入detail表
//                var rt_sectionkey = await _sectionKeyService.InsertAsync(secKey);
//                if (!rt_sectionkey.Item1) throw new Exception("保存section key失败");
//                // 获取到当前key的模板(keydetail)详细
//                var secdetails = templetdetaillist.Where(t => t.KeyId == key.Id);
//                // 获取到当前模板的game模板配置详细
//                var gamedetails = templetgamelist.Where(t => t.GameStr == h5Templet);
//                var pcImgUrl = string.Empty;
//                var h5ImgUrl = string.Empty;
//                List<SectionDetail> secdList = new List<SectionDetail>();
//                foreach (var sd in secdetails)
//                {
//                    if (!sd.PageUrl.IsNullOrEmpty())
//                    {
//                        // 找出对应模板里面的游戏
//                        var g = gamedetails.Where(t => t.GameStr == sd.PageUrl).SingleOrDefault();
//                        if (g != null)
//                        {
//                            pcImgUrl = g.PcImgStr;
//                            h5ImgUrl = g.H5ImgStr;
//                        }
//                    }

//                    var secdetail = new SectionDetail()
//                    {
//                        SectionId = rt_sectionkey.Item3,
//                        MerchantId = merchantId,
//                        Name = sd.Name,
//                        Alias = sd.Alias,
//                        Enabled = sd.Enabled,
//                        HasSubMenu = sd.HasSubMenu,
//                        SKey = sd.SKey,
//                        PcImgUrl = pcImgUrl,
//                        H5ImgUrl = h5ImgUrl,
//                        PageUrl = sd.PageUrl,
//                        Tcontent = sd.Tcontent,
//                        SortNo = 0
//                    };
//                    secdList.Add(secdetail);
//                }

//                await _sectionDetailService.InsertAsync(secdList);
//            }
//            try
//            {
//                // TODO
//                // 4. 完成站点创建
//                // 5. 创建admin用户以及所有权限
//                await _sysAccountService.CreateMerchantAdminDefaultAccountAsync(merchantId, "bwMerchant");

//                // 6. 创建用户组
//                var group_rt = await _vipGroupsService.CreateMerchantAdminDefaultVIPGroupsAsync(merchantId);

//                // 7. 创建默认代理用户 user member
//                await _usersService.CreateMerchantAdminDefaultMemberAsync(merchantId, group_rt.Item3);

//                // 8. 初始化代理规则
//            }
//            catch (Exception ex)
//            {
//                Console.Write(ex.Message);
//            }


//            return true;
//        }


//        /// <summary>
//        /// 同步游戏的默认图片，站点不存在的游戏则默认添加
//        /// </summary>
//        /// <returns></returns>
//        public async Task<bool> SynchronizeGameDefaultImage()
//        {
//            _templetKeyService = (ITempletKeyService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletKeyService));
//            _templetDetailService = (ITempletDetailService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletDetailService));
//            _templetGameService = (ITempletGameService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletGameService));


//            // 1. 获取到所有的站点
//            var merchants = await _merchantService.GetMerchantsAsync();

//            foreach (var mch in merchants)
//            {
//                // 2. 获取到所有的游戏数据信息
//                var gamesPc = await _templetGameService.GetListAsync(mch.PcTempletStr);
//                var gamesH5 = await _templetGameService.GetListAsync(mch.H5TempletStr);

//                var secDetail = await _sectionDetailService.GetByMerchantIdAsync(mch.Id);

//                // 3. 比对数据进行写入,只更新 PC与H5的图片西信息
//                bool update = false;
//                foreach (var secd in secDetail)
//                {
//                    var pc = gamesPc.Where(t => t.GameStr == secd.PageUrl).SingleOrDefault();
//                    if (pc != null && secd.PcImgUrl.IsNullOrEmpty())
//                    {
//                        secd.PcImgUrl = pc.PcImgStr;
//                        update = true;
//                    }
//                    var h5 = gamesH5.Where(t => t.GameStr == secd.PageUrl).SingleOrDefault();
//                    if (h5 != null && secd.H5ImgUrl.IsNullOrEmpty())
//                    {
//                        secd.H5ImgUrl = h5.H5ImgStr;
//                        update = true;
//                    }

//                    if (update)
//                        await _sectionDetailService.InsertOrUpdateAsync(new SectionDetailsModifyModel()
//                        {
//                            Id = secd.Id,
//                            Alias = secd.Alias,
//                            Enabled = secd.Enabled,
//                            HasSubMenu = secd.HasSubMenu,
//                            PageUrl = secd.PageUrl,
//                            PcImgUrl = secd.PcImgUrl,
//                            H5ImgUrl = secd.H5ImgUrl,
//                            SortNo = secd.SortNo,
//                            SKey = secd.SKey
//                        });
//                }
//            }
//            return true;
//        }


//        /// <summary>
//        /// 同步新增的游戏菜单 到站点菜单项
//        /// </summary>
//        /// <returns></returns>
//        public async Task<bool> SynchronizeNewGameToSiteMenu()
//        {
//            _templetKeyService = (ITempletKeyService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletKeyService));
//            _templetDetailService = (ITempletDetailService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletDetailService));
//            _templetGameService = (ITempletGameService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletGameService));
//            ISectionKeyRepository _secKeyRep = (ISectionKeyRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ISectionKeyRepository));
//            ISectionDetailRepository _secDetailRep = (ISectionDetailRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ISectionDetailRepository));
//            var secDetailsList = await _secDetailRep.GetListAsync();
//            // 1. 获取到所有的站点
//            var merchants = await _merchantService.GetMerchantsAsync();
//            foreach (var mch in merchants)
//            {
//                // 获取到站点的子菜单
//                var menu = secDetailsList.Where(t => t.MerchantId == mch.Id & !t.PageUrl.IsNullOrEmpty()).ToList();
//                // 获取到所有的游戏
//                var templets = (await _templetDetailService.GetListAsync(mch.PcTempletStr)).Item2.Where(t => !t.PageUrl.IsNullOrEmpty()).ToList();
//                foreach (var t in templets)
//                {
//                    if (t.PageUrl.Contains("_"))
//                    {
//                        if (!menu.Select(t => t.PageUrl).Contains(t.PageUrl))
//                        {
//                            var keyId = await GetKeyId(t.SKey, mch.Id);
//                            // 写入数据到SectionDetails
//                            await _sectionDetailService.InsertOrUpdateAsync(new SectionDetailsModifyModel()
//                            {
//                                MerchantId = mch.Id,
//                                KeyId = keyId,
//                                Alias = t.Alias,
//                                Enabled = t.Enabled,
//                                HasSubMenu = t.HasSubMenu,
//                                PageUrl = t.PageUrl,
//                                PcImgUrl = t.PcImgUrl,
//                                H5ImgUrl = t.H5ImgUrl,
//                                SortNo = 0,
//                                SKey = t.SKey
//                            });
//                        }
//                    }
//                }
//            }
//            return true;
//        }


//        /// <summary>
//        /// 同步指定菜单到站点站点
//        /// </summary>
//        /// <param name="templeteDetailId"></param>
//        /// <returns></returns>
//        public async Task<bool> SynchronizeNewMenuToSiteMenu(int templeteDetailId)
//        {
//            ITempletKeyRepository tmpKeyRep = (ITempletKeyRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletKeyRepository));
//            ITempletDetailRepository tmpRep = (ITempletDetailRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITempletDetailRepository));
//            ISectionKeyRepository _secKeyRep = (ISectionKeyRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ISectionKeyRepository));
//            ISectionDetailRepository _secDetailRep = (ISectionDetailRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ISectionDetailRepository));

//            var templ = tmpRep.Get(templeteDetailId); // 获取到当前模板在sys的配置
//            if (templ == null) return false;
//            var templKey = tmpKeyRep.Get(templ.KeyId); // 获取到当前模板的key内容


//            var merchants = await _merchantService.GetMerchantsAsync();
//            foreach (var mch in merchants)
//            {

//                var secKey = await _secKeyRep.GetByKeyAsync(mch.Id, templKey.SKey);// 获取到模板key 在sectonKey的位置
//                if (secKey == null) // 如果key在SectionKey不存在,那么新在商户新增这个key
//                {
//                    var key = new SectionKey()
//                    {
//                        MerchantId = mch.Id,
//                        Type = templKey.Type.ToString().ToEnum<KeyType>().Value,
//                        DetailType = templKey.DetailType.ToString().ToEnum<KeyDetailType>().Value,
//                        SKey = templKey.SKey,
//                        Description = templKey.Description
//                    };
//                    var key_id = await _secKeyRep.InsertWithCacheAsync(key);
//                    if (key_id == null || key_id.Value == 0) throw new Exception($"数据插入错误{key.ToJson()}");
//                    secKey = _secKeyRep.Get(key_id.Value);
//                }
//                await _sectionDetailService.InsertOrUpdateAsync(new SectionDetailsModifyModel()
//                {
//                    MerchantId = mch.Id,
//                    KeyId = secKey.Id,
//                    Alias = templ.Alias,
//                    Enabled = templ.Enabled,
//                    HasSubMenu = templ.HasSubMenu,
//                    PageUrl = templ.PageUrl,
//                    PcImgUrl = templ.PcImgUrl,
//                    H5ImgUrl = templ.H5ImgUrl,
//                    SortNo = 0,
//                    SKey = templ.SKey,
//                });
//            }
//            return true;
//        }


//        private async Task<int> GetKeyId(string key, int merchantId)
//        {
//            ISectionKeyRepository _secKeyRep = (ISectionKeyRepository)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ISectionKeyRepository));
//            var keyd = await _secKeyRep.GetByKeyAsync(merchantId, key);
//            if (keyd == null) // 如果key也是新增，那么新增key
//            {
//                var secKey = new SectionKey()
//                {
//                    MerchantId = merchantId,
//                    Type = KeyType.PC,
//                    DetailType = KeyDetailType.Multi,
//                    SKey = key,
//                    Description = key
//                };
//                await _secKeyRep.InsertWithCacheAsync(secKey);
//                return (await _secKeyRep.GetByKeyAsync(merchantId, key)).Id;
//            }
//            return keyd.Id;
//        }

//    }
//}
