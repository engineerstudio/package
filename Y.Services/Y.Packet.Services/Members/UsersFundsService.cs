using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Members;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;

namespace Y.Packet.Services.Members
{
    /// <summary>
    /// 用户的资金加减，用户日志操作
    /// </summary>
    public class UsersFundsService : IUsersFundsService
    {
        private readonly IUsersFundsRepository _repository;
        private readonly IUsersFundsLogRepository _usersFundsLogRepository;

        public UsersFundsService(IUsersFundsRepository repository, IUsersFundsLogRepository usersFundsLogRepository)
        {
            _repository = repository;
            _usersFundsLogRepository = usersFundsLogRepository;
        }


        public async Task<UsersFunds> GetAsync(int merchantId, int userId) => await _repository.GetAsync(merchantId, userId);


        /// <summary>
        /// 用户的可用资金  总金额-锁定金额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetUserAvailableFundsAsync(int merchantId, int userId)
        {
            return await _repository.GetUserAvailableFundsAsync(merchantId, userId);
        }

        /// <summary>
        /// 获取当前账户总金额 ， 包含可用与锁定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetUserFundsAsync(int merchantId, int userId)
        {
            return await _repository.GetUserFundsAsync(merchantId, userId);
        }


        public async Task<decimal> GetUserLockFundsAsync(int merchantId, int userId)
        {
            return await _repository.GetUserLockFundsAsync(merchantId, userId);
        }


        #region 活动金额

        /// <summary>
        /// [活动金额] 
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> PromoRewardAsync(int merchantId, int memberId, int orderId, decimal amount, string mark, FundLogType_Promotions type)
        {
            // 判断该订单是否已经存在
            var exist = await _usersFundsLogRepository.ExistFundsLogSourceIdAsync(merchantId, memberId, orderId.ToString(), FundLogType.Promotions);
            if (exist) return (false, "该订单已经存在");

            var m = await _repository.GetAsync(merchantId, memberId);
            // 加款总金额
            m.TotalFunds += amount;
            // 2. 更新UsersFundsLog表数据

            string sourceId = $"{FundLogType.Promotions.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}"; // 更换为订单的ID
            string marks = mark;
            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, amount, m.LockFunds, m.TotalFunds, FundLogType.Promotions, (byte)type.GetEnumValue(), TransType.In, sourceId, "127.0.0.1", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);

            return (rt, null);
        }

        #endregion


        #region 游戏金额

        /// <summary>
        /// 游戏资金解锁
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="sucess"></param>
        /// <returns></returns>
        public async Task<(bool, string)> GameUnlockAsync(int merchantId, int memberId, int orderId, decimal amount, bool sucess)
        {
            //// 1. 获取会员资金信息
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);

            //// 2. 减去锁定资金, 会员金额减去金额
            //if (sucess)
            //    m.TotalFunds -= Math.Abs(amount);
            //m.LockFunds -= Math.Abs(amount);

            //string sourceId = $"{FundLogType.Games.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}";  // 更换为订单的ID
            //string marks = $"游戏解锁:{orderId}";
            //// 更新UsersFundsLog表数据
            //var log = CreateFundsLogModel(merchantId, memberId, -amount, m.LockFunds, m.TotalFunds, FundLogType.WithDraw, (byte)FundLogType_WithDraw.Channel.GetEnumValue(), TransType.Out, sourceId, "ip", marks);

            //var rt = await _repository.UpdateUserFundsAsync(m, log);
            //return (rt, null);


            string sourceId = $"{FundLogType.Games.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}";  // 更换为订单的ID
            string marks = $"游戏解锁:{orderId}";

            return await UnlockMoneyAsync(merchantId, memberId, orderId, amount, sourceId, marks, sucess, false);
        }

        /// <summary>
        /// 解锁公共方法
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="sourceId"></param>
        /// <param name="marks"></param>
        /// <param name="sucess">是否操作成功  true:成功</param>
        /// <param name="isWithdrawal">是否是出款  true:是</param>
        /// <returns></returns>
        private async Task<(bool, string)> UnlockMoneyAsync(int merchantId, int memberId, int orderId, decimal amount, string sourceId, string marks, bool sucess, bool isWithdrawal)
        {
            // 1. 获取会员资金信息
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);

            var m = await _repository.GetAsync(merchantId, memberId);
            // 2. 减去锁定资金, 会员金额减去金额
            if (sucess)
                m.TotalFunds -= Math.Abs(amount);
            m.LockFunds -= Math.Abs(amount);

            // 3. 操作成功  并且  是出款操作.  游戏操作只有解锁
            if (sucess && isWithdrawal)
            {
                m.TotalWithdrawalFunds += Math.Abs(amount);
                m.TotalWithdrawalCount += 1;
            }

            //string sourceId = $"{FundLogType.Games.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}";  // 更换为订单的ID
            //string marks = $"游戏解锁:{orderId}";
            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, -amount, m.LockFunds, m.TotalFunds, FundLogType.WithDraw, (byte)FundLogType_WithDraw.Channel.GetEnumValue(), TransType.Out, sourceId, "ip", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);
            return (rt, null);
        }

        /// <summary>
        /// 转账到游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="transferLogId"></param>
        /// <param name="gameType"></param>
        /// <param name="subType"></param>
        /// <param name="amount"></param>
        /// <param name="lockAmount"></param>
        /// <returns></returns>
        public async Task<(bool, string)> TransInGameAsync(int merchantId, int memberId, int transferLogId, GameType gameType, FundLogType_Games subType, decimal amount, decimal lockAmount, bool sucess)
        {

            if (memberId == 0) return (false, "用户不存在");
            if (merchantId == 0) return (false, "商户不存在");
            //if (string.IsNullOrEmpty(sourceId)) return (false, "请输入来源标识");
            amount = Math.Abs(amount);
            string sourceId = transferLogId.ToString();
            string marks = $"{FundLogType.Games.GetDescription()}:{subType.GetDescription()}";

            // 1. 获取用户资金
            string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            var m = await _repository.GetAsync(where);
            // 减款总金额
            if (sucess)
                m.TotalFunds -= amount;
            m.LockFunds += lockAmount;


            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, amount, m.LockFunds, m.TotalFunds, FundLogType.Games, (byte)subType, TransType.In, sourceId, "ip", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);
            return (rt, null);

        }

        /// <summary>
        /// 从游戏转出
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberid"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<(bool, string)> TransOutGameAsync(int merchantId, int memberId, int transferLogId, GameType gameType, FundLogType_Games subType, decimal amount, decimal lockAmount, bool sucess)
        {

            if (memberId == 0) return (false, "用户不存在");
            if (merchantId == 0) return (false, "商户不存在");
            //if (string.IsNullOrEmpty(sourceId)) return (false, "请输入来源标识");
            amount = Math.Abs(amount);
            string sourceId = transferLogId.ToString();
            string marks = $"{FundLogType.Games.GetDescription()}:{subType.GetDescription()}";

            // 1. 获取用户资金
            string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            var m = await _repository.GetAsync(where);
            // 减款总金额
            if (sucess)
                m.TotalFunds += amount;
            m.LockFunds += lockAmount;

            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, amount, m.LockFunds, m.TotalFunds, FundLogType.Games, (byte)subType, TransType.Out, sourceId, "ip", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);
            return (rt, null);


        }


        #endregion


        #region 出款

        /// <summary>
        ///  会员出款成功， 资金解锁 , 扣除会员资金
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="sucess">是否出款成功：true->是，false->否,仅解锁</param>
        /// <returns></returns>
        public async Task<(bool, string)> WithdrawalUnlockAsync(int merchantId, int memberId, int orderId, decimal amount, bool sucess)
        {
            //// 1. 获取会员资金信息
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);

            //// 2. 减去锁定资金, 会员金额减去金额
            //if (sucess)
            //    m.TotalFunds -= Math.Abs(amount);
            //m.LockFunds -= Math.Abs(amount);

            //string sourceId = $"{FundLogType.WithDraw.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}";  // 更换为订单的ID
            //string marks = $"出款解锁:{orderId}";
            //// 更新UsersFundsLog表数据
            //var log = CreateFundsLogModel(merchantId, memberId, -amount, m.LockFunds, m.TotalFunds, FundLogType.WithDraw, (byte)FundLogType_WithDraw.Channel.GetEnumValue(), TransType.Out, sourceId, "ip", marks);

            //var rt = await _repository.UpdateUserFundsAsync(m, log);
            //return (rt, null);


            string sourceId = $"{FundLogType.WithDraw.ToString()}-{merchantId}-{memberId}-{orderId}";  // 更换为订单的ID
            string marks = $"出款解锁:{orderId}";

            var exist = await _usersFundsLogRepository.ExistFundsLogSourceIdAsync(merchantId, memberId, sourceId, FundLogType.WithDraw);
            if (exist) return (false, "该转账已经解锁");

            return await UnlockMoneyAsync(merchantId, memberId, orderId, amount, sourceId, marks, sucess, true);
        }


        /// <summary>
        /// 提交出款就会 锁定资金
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> WithdrawalLockAsync(int merchantId, int memberId, int orderId, decimal amount)
        {
            // 1. 获取会员资金信息
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);

            var m = await _repository.GetAsync(merchantId, memberId);

            // 2. 增加锁定资金
            m.LockFunds += Math.Abs(amount);

            string sourceId = $"{FundLogType.WithDraw.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}";  // 更换为订单的ID
            string marks = $"出款锁定:{orderId}";
            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, -amount, m.LockFunds, m.TotalFunds, FundLogType.WithDraw, (byte)FundLogType_WithDraw.Channel.GetEnumValue(), TransType.Out, sourceId, "ip", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);
            return (rt, null);
        }



        /// <summary>
        /// 手动取款/取款成功 后 操作金额  . 1.减去锁定资金 and 用户资金 2.增加出款金额 3.增加出款次数
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<(bool, string)> WithdrawalAsync(int merchantId, int memberId, int orderId, decimal amount, string withDrawMerchantName)
        {
            // 1. 更新UsersFunds表数据
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);
            var m = await _repository.GetAsync(merchantId, memberId);

            // 减款总金额
            m.TotalFunds -= amount;
            //// 解除锁定资金
            m.LockFunds -= amount;
            // 增加出款金额汇总
            m.TotalWithdrawalFunds += amount;
            // 增加出款次数
            m.TotalWithdrawalCount += 1;

            if (m.TotalFunds < 0) return (false, "你太牛逼了，竟然搞成负数");

            string sourceId = $"{FundLogType.WithDraw.ToString()}-{merchantId}-{memberId}-{orderId}";  // 更换为订单的ID
            string marks = $"渠道减款:{withDrawMerchantName}";

            // 判断是否已经出款
            var existLogs = await _usersFundsLogRepository.ExistFundsLogSourceIdAsync(merchantId, memberId, sourceId, FundLogType.WithDraw);
            if (existLogs) return (false, "该笔订单已经出款");

            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, -amount, m.LockFunds, m.TotalFunds, FundLogType.WithDraw, (byte)FundLogType_WithDraw.Manual.GetEnumValue(), TransType.Out, sourceId, "ip", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);

            return (rt, null);
        }


        #endregion

        #region 充值

        /// <summary>
        /// 充值成功后的操作 1.更新表UsersFunds金额数据  2.增加一条UsersFundsLog数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<(bool, string)> RechargeAsync(int merchantId, int memberId, int orderId, decimal amount, string payMerchantName)
        {
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            var m = await _repository.GetAsync(merchantId, memberId);
            // 加款总金额
            m.TotalFunds += amount;
            // 解除锁定资金  没有锁定资金 不要减去
            //m.LockFunds = m.LockFunds - amount;
            // 增加加款金额汇总
            m.TotalRechargedFunds += amount;
            // 增加加款次数
            m.TotalRechargedFundsCount += 1;
            // 2. 更新UsersFundsLog表数据

            string sourceId = $"{FundLogType.Recharge.ToString()}-{DateTime.Now.ToString("yyyyMMddHHmmssms")}-{orderId}"; // 更换为订单的ID
            string marks = $"渠道加款:{payMerchantName}";
            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, amount, m.LockFunds, m.TotalFunds, FundLogType.Recharge, (byte)FundLogType_Recharge.Channel.GetEnumValue(), TransType.In, sourceId, "ip", marks);

            var rt = await _repository.UpdateUserFundsAsync(m, log);

            return (rt, null);
        }


        #endregion

        #region 手动加减款


        /// <summary>
        /// 手动加款
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<(bool, string)> AddManualAddFundsAsync(FundLogType logtype, byte subType, string subTypeDesc, int merchantId, int memberId, decimal amount, string sourceId = null, string marks = null)
        {
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);
            var m = await _repository.GetAsync(merchantId, memberId);
            // 加款总金额
            m.TotalFunds = m.TotalFunds += amount;
            // 增加加款金额汇总
            m.TotalRechargedFunds += amount;
            // 增加加款次数
            m.TotalRechargedFundsCount += 1;

            sourceId = sourceId == null ? $"{logtype.ToString()}_Manual_{DateTime.Now.ToString("yyyyMMddHHmmssms")}" : sourceId;
            marks = marks == null ? $"手动加款操作:{logtype.GetDescription()},{subTypeDesc}" : marks;
            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, amount, m.LockFunds, m.TotalFunds, logtype, subType, TransType.In, sourceId, "ip", marks);
            //log.MerchantId = merchantId;
            //log.UserId = memberId;
            //log.Amount = amount;
            //log.LockedAmount = 0;
            //log.Balance = m.TotalFunds;
            //log.FundsType = logtype;
            //log.SubTundsType = subType;
            //log.TransType = TransType.In;
            //log.SourceId = $"{logtype.ToString()}_Manual_{DateTime.Now.ToString("yyyyMMddHHmmssms")}";
            //log.IP = "ip";
            //log.Marks = $"加款操作:{logtype.GetDescription()},{subTypeDesc}";
            //log.CreateTime = DateTime.UtcNow.AddHours(8);

            var rt = await _repository.UpdateUserFundsAsync(m, log);

            return (rt, sourceId);
        }



        /// <summary>
        /// 手动减款
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount">大于零的数字</param>
        /// <returns></returns>
        public async Task<(bool, string)> AddManualMinusFundsAsync(FundLogType logtype, byte subType, string subTypeDesc, int merchantId, int memberId, decimal amount, string sourceId = null, string marks = null)
        {
            // 1. 更新UsersFunds表数据
            //string where = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId={memberId}";
            //var m = await _repository.GetAsync(where);
            var m = await _repository.GetAsync(merchantId, memberId);
            // 减款总金额
            m.TotalFunds = m.TotalFunds -= amount;
            // 增加出款金额汇总
            m.TotalWithdrawalFunds += amount;
            // 增加出款次数
            m.TotalWithdrawalCount += 1;

            sourceId = sourceId == null ? $"{logtype}_Manual_{DateTime.Now.ToString("yyyyMMddHHmmssms")}" : sourceId;
            marks = marks == null ? $"手动减款操作:{logtype.GetDescription()},{subTypeDesc}" : marks;
            // 更新UsersFundsLog表数据
            var log = CreateFundsLogModel(merchantId, memberId, amount, m.LockFunds, m.TotalFunds, logtype, subType, TransType.Out, sourceId, "ip", marks);
            //log.MerchantId = merchantId;
            //log.UserId = memberId;
            //log.Amount = amount;
            //log.LockedAmount = 0;
            //log.Balance = m.TotalFunds;
            //log.FundsType = logtype;
            //log.SubTundsType = subType;
            //log.TransType = TransType.Out;
            //log.SourceId = $"{logtype}_Manual_{DateTime.Now.ToString("yyyyMMddHHmmssms")}";
            //log.IP = "ip";
            //log.Marks = $"减款操作:{logtype.GetDescription()},{subTypeDesc}";
            //log.CreateTime = DateTime.UtcNow.AddHours(8);


            var rt = await _repository.UpdateUserFundsAsync(m, log);
            return (rt, null);
        }

        #endregion


        #region 公共方法

        /// <summary>
        /// 创建资金日志
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="memberId">会员ID</param>
        /// <param name="amount">账变金额</param>
        /// <param name="lockedAmount">锁定金额</param>
        /// <param name="totalFunds"></param>
        /// <param name="logtype">账变后金额/余额</param>
        /// <param name="subType">账变子类型</param>
        /// <param name="transType">账变类型 转入转出</param>
        /// <param name="SourceId">源ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="marks">备注</param>
        /// <returns></returns>
        private UsersFundsLog CreateFundsLogModel(int merchantId, int memberId, decimal amount, decimal lockedAmount, decimal totalFunds, FundLogType logtype, byte subType, TransType transType, string SourceId, string ip, string marks)
        {
            return new UsersFundsLog()
            {
                MerchantId = merchantId,
                MemberId = memberId,
                Amount = amount,
                LockedAmount = lockedAmount,
                Balance = totalFunds,
                FundsType = logtype,
                SubFundsType = subType,
                TransType = transType,
                SourceId = SourceId,
                IP = ip,
                Marks = marks,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };
        }

        public (byte, string, string) GetAttributeSubType(string typeStr, string typeValue)
        {
            if (string.IsNullOrEmpty(typeStr) || string.IsNullOrEmpty(typeValue)) throw new Exception("不可为空的类型");
            switch (typeStr)
            {
                case "FundLogType_Recharge":
                    FundLogType_Recharge? d = typeValue.ToEnum<FundLogType_Recharge>();
                    if (d == null) throw new Exception("类型不存在");
                    return ((byte)d.GetEnumValue(), d.ToString(), d.GetDescription());
                case "FundLogType_WithDraw":
                    FundLogType_WithDraw? d2 = typeValue.ToEnum<FundLogType_WithDraw>();
                    if (d2 == null) throw new Exception("类型不存在");
                    return ((byte)d2.GetEnumValue(), d2.ToString(), d2.GetDescription());
                case "FundLogType_Promotions":
                    FundLogType_Promotions? d3 = typeValue.ToEnum<FundLogType_Promotions>();
                    if (d3 == null) throw new Exception("类型不存在");
                    return ((byte)d3.GetEnumValue(), d3.ToString(), d3.GetDescription());
                case "FundLogType_Games":
                    FundLogType_Games? d4 = typeValue.ToEnum<FundLogType_Games>();
                    if (d4 == null) throw new Exception("类型不存在");
                    return ((byte)d4.GetEnumValue(), d4.ToString(), d4.GetDescription());
                default:
                    throw new Exception("类型不存在");
            }
        }

        #endregion
    }
}