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
*│　描    述：投注日志表                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-09-07 16:30:43                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.IGames                                   
*│　接口名称： IGameLogsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Repositories.IGames
{
    public interface IGameLogsRepository : IBaseRepository<GameLogs, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        /// <summary>
        /// 是否表中已经存在该游戏订单
        /// </summary>
        /// <param name="gameTypeStr"游戏类型></param>
        /// <param name="sourceId">游戏源ID</param>
        /// <returns>是否已经存在,存在返回实体类</returns>
        Task<(bool, GameLogs)> ExistLogAsync(string gameTypeStr, string sourceId);

        /// <summary>
        /// 获取汇总 投注数据、有效投注、派奖金额、盈亏
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        Task<Dictionary<string, decimal>> GetAmountSumAsync(string conditions, DynamicParameters parms);

        /// <summary>
        /// 生成日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IEnumerable<GameDailyModel>> GetDailySumAsync(string date);

        /// <summary>
        /// 生成指定用户日报表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IEnumerable<GameDailyModel>> GetDailySumAsync(int merchantId, int memberId, string date);


        /// <summary>
        /// 日报表数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        Task<Dictionary<string, object>> CreateDailyAgentDataAsync(int merchantId, string date, IEnumerable<int> memberIds);

        /// <summary>
        /// [周签到活动]获取上周有效投注
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetTaskLaskWeekValidBetAsync(int merchantId, int userId);

        /// <summary>
        /// 获取过去两分钟内完成的日志
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<GameLogsForWashOrdersModel>> GetTheLast2MinutesFinishedLogsAsync(int logId = 0);

    }
}