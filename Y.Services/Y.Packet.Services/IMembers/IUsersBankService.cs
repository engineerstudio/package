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
*│　描    述：用户银行卡绑定                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-30 15:00:55                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.IServices                                   
*│　接口名称： IUsersBankRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;

namespace Y.Packet.Services.IMembers
{
    public interface IUsersBankService
    {

        Task<(bool, string)> InsertAsync(int merchantId, int userId, string accountName, string accountNumber);


        Task<(bool, string, UsersBank)> GetAsync(int id);

        Task<IEnumerable<UsersBank>> GetListAsync(int merchantId, int memberId);


        Task<(IEnumerable<UsersBank>, int)> GetPageListAsync(int merchantId, int memberId);

        /// <summary>
        /// [绑定银行卡活动]获取过去8分钟内绑定银行卡的会员
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UsersBank>> GetTaskListAsync();
    }
}