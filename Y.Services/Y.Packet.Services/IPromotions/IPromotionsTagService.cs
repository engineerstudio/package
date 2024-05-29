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
*│　接口名称： IPromotionsTypeRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;

namespace Y.Packet.Services.IPromotions
{
    public interface IPromotionsTagService
    {
        Task<(IEnumerable<PromotionsTag>, int)> GetPageListAsync(PromotionsTagListPageQuery q);

        Task<(bool, string)> InsertOrModifyAsync(PromotionsTagAddOrModifyModel m);

        Task<(bool, string)> DeleteAsync(int merchantId, int tagId);

        /// <summary>
        /// 获取 ID/Name 的字典
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, Dictionary<int, string>)> GetDicAsync(int merchantId);
        Task<(bool, IEnumerable<PromotionsTag>)> GetListsAsync(int merchantId);
    }
}