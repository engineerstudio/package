////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：用户资金日志                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-30 15:00:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Members                                  
*│　类    名： UsersFundsLogService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;

namespace Y.Packet.Services.Members
{
    public class UsersFundsLogService : IUsersFundsLogService
    {
        private readonly IUsersFundsLogRepository _repository;
        private readonly IUsersFundsRepository _usersFundsRepository;

        public UsersFundsLogService(IUsersFundsLogRepository repository, IUsersFundsRepository usersFundsRepository)
        {
            _repository = repository;
            _usersFundsRepository = usersFundsRepository;
        }


        /// <summary>
        /// 游戏转账
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="merchantId"></param>
        /// <param name="money"></param>
        /// <param name="sourceId"></param>
        /// <param name="fundsType"></param>
        /// <param name="subType"></param>
        /// <param name="transType"></param>
        /// <param name="desc"></param>
        /// <param name="ip"></param>
        /// <param name="transSucess">转账是否成功/等待确认状态</param>
        /// <returns></returns>
        public async Task<(bool, UsersFundsLog, string)> AddFundsLogAsync(int userId, int merchantId, decimal money, string sourceId, FundLogType fundsType, byte subType, TransType transType, string desc, string ip, bool transSucess = true)
        {
            if (userId == 0) return (false, null, "用户不存在");
            if (merchantId == 0) return (false, null, "商户不存在");
            if (string.IsNullOrEmpty(sourceId)) return (false, null, "请输入来源标识");
            if (subType == 0) return (false, null, "资金子类型错误");

            decimal balance, lockedAmount;
            // 判断转账类型 用户是否存在金额
            if (transType == TransType.Out)
            {
                // 获取用户余额
                balance = await _usersFundsRepository.GetUserAvailableFundsAsync(merchantId,userId);
                if (balance < money) return (false, null, "余额不足,请充值");
            }
            balance = await _usersFundsRepository.GetUserFundsAsync(merchantId,userId);
            lockedAmount = await _usersFundsRepository.GetUserLockFundsAsync(merchantId, userId);
            // 判断商户/用户/sourceId/转账类型是否已经存在
            if (await _repository.ExistFundsLogSourceIdAsync(merchantId, userId, sourceId, transType)) return (false, null, "该数据已经存在");

            money = transType == TransType.In ? Math.Abs(money) : Math.Abs(money) * -1;
            var log = new UsersFundsLog()
            {
                MerchantId = merchantId,
                MemberId = userId,
                Amount = money,
                LockedAmount = lockedAmount,
                Balance = balance,
                FundsType = fundsType,
                SubFundsType = subType,
                TransType = transType,
                SourceId = sourceId,
                IP = ip,
                Marks = desc,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };
            var rt = await _repository.InsertByTransAsync(log, transSucess);
            return (rt, log, rt ? "保存成功" : "保存失败");
        }


        public async Task<(IEnumerable<UsersFundsLog>, int)> GetPageListAsync(FundsLogQuery q)
        {
            var parm = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (q.MemberId != 0)
                conditions += $" AND MemberId={q.MemberId} ";
            if (q.FundLogType != null)
                conditions += $" AND FundsType={(int)q.FundLogType} ";
            if (q.TransType != null)
                conditions += $" AND TransType={(int)q.TransType} ";
            if (q.StartAt != null && q.EndAt != null)
                conditions += $" AND CreateTime Between N'{q.StartAt}' AND N'{q.EndAt}' ";

            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parm), _repository.RecordCount(conditions, parm));
        }


        public async Task<bool> ExistLogAsync(int logId, int merchantId, int memberId, GameType gameType)
        {
            var rt = await _repository.ExistLogAsync(logId, merchantId, memberId, gameType);
            return rt > 0;
        }

    }
}