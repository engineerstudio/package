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
*│　描    述：                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-07-26 00:31:01                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IPromotions                                   
*│　接口名称： IActivityRecordsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;

namespace Y.Packet.Services.IPromotions
{
    public interface IActivityOrdersService
    {

        Task<(bool, string, int)> InsertAsync(ActivityOrders records);
        Task<(bool, string, int)> InsertAsync(ActivityOrderAddModel model);
        Task<(IEnumerable<ActivityOrders>, int)> GetListAsync(ActivityOrdersListPageQuery q);

        /// <summary>
        /// 检查是否满足活动要求 
        ///   1. 是否重复参与活动
        ///   2. 是否同Ip活动
        ///   3. 活动是否启动或者开始
        ///   4. 流水要求
        ///   5. 充值要求
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="promotionConfigId"></param>
        /// <param name="ip"></param>
        /// <param name="effectiveMoney"></param>
        /// <param name="chargeMoney"></param>
        /// <returns></returns>
        (bool, string) CheckInsertConditions(int siteId, int userId, ActivityType type, int promotionConfigId, string ip, decimal effectiveMoney, decimal chargeMoney);


        /// <summary>
        /// 判断指定的用户订单是否参与过指定的活动
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="activityType"></param>
        /// <returns></returns>
        Task<bool> ExistOrdersAsync(int merchantId, int userId, ActivityType activityType);

        /// <summary>
        /// [生日礼金] 判断是否存在在过去360天内发放过，需要根据备注字段进行查询，生日礼金备注字段是固定的
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="dt"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<bool> ExistBirthOrdersAsync(int merchantId, int userId, DateTime dt, string description);
        Task<bool> ExistSourceIdAsync(string sourceId);


        /// <summary>
        /// [活动列表->订单汇总]获取订单汇总
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        Task<(IEnumerable<ActivityOrderSummary>, int)> GetOrderSummary(int merchantId, int promoId);

    }
}