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
*│　命名空间： Y.Packet.Services.IMerchants                                   
*│　接口名称： IHelpAreaTypeRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;

namespace Y.Packet.Services.IMerchants
{
    public interface IHelpAreaTypeService
    {

        Task<Dictionary<int, string>> GetDicAsync(int merchantId);

        Task<(bool, string)> InsertOrModifyAsync(HelpAreaTypeAOM aom);

        Task<(IEnumerable<HelpAreaType>, int)> GetPageListAsync(int merchantId);

        Task<(bool, string)> DeleteAsync(int merchantId, int id);
        /// <summary>
        /// 获取到帮助列表及下级
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<HelpAreaTypeVM2>> GetHelpAreaTypesAsync(int merchantId);
        /// <summary>
        /// 获取到带图片的帮助列表及下级
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<HelpAreaTypeVM3>> GetHelpAreaTypesWithCategoryIconImagesAsync(int merchantId);
    }
}