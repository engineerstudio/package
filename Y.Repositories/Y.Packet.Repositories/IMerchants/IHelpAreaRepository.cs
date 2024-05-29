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
*│　创建时间：2021-03-02 22:13:21                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.IMerchants                                   
*│　接口名称： IHelpAreaRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Merchants;


namespace Y.Packet.Repositories.IMerchants
{
    public interface IHelpAreaRepository : IBaseRepository<HelpArea, Int32>
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
        /// 获取商户所有配置的帮助类别
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetTypeIdsAsync(int merchantId);

        /// <summary>
        /// 获取是否存在数量
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> GetDataCountAsync(int merchantId, int id);

        /// <summary>
        /// 获取到前台展示的项
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<HelpArea>> GetShowHelpAreaAsync(int merchantId);
    }
}