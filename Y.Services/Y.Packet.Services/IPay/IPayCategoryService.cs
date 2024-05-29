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
*│　创建时间：2020-09-30 16:19:19                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IPay                                   
*│　接口名称： IPayCategoryRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;

namespace Y.Packet.Services.IPay
{
    public interface IPayCategoryService
    {
        /// <summary>
        /// 获取所有的支付类型
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAllPayTypeAsync();

        /// <summary>
        /// 获取支付类型字典
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetCategoryDicAsync();

        /// <summary>
        /// 异步插入支付类型接口
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        Task<(bool, string)> InsertAsync(KeyValuePair<string, string> kvp);


        Task<(IEnumerable<PayCategory>, int)> GetPageListAsync(PayListPageQuery q);


        Task<(bool, string)> UpdateConfigAsync(int id, string configStr);

        /// <summary>
        /// 根据ID获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool, string, PayCategory)> GetByIdAsync(int id);

    }
}