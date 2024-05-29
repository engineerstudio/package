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
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-24 02:23:05                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Sys.Service                                  
*│　类    名： SysRolePermissionService                                    
*└──────────────────────────────────────────────────────────────┘
*/

using System.Collections.Generic;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using Y.Infrastructure.Library.Core.AuthController.IService;


namespace Y.Infrastructure.Library.Core.AuthController.Service
{
    public class SysRolePermissionService : ISysRolePermissionService
    {
        private readonly ISysRolePermissionRepository _repository;

        public SysRolePermissionService(ISysRolePermissionRepository repository)
        {
            _repository = repository;
        }

        public int[] GetIdsByRoleId(int RoleId)
        {
            if (RoleId <= 0) return new int[0];

            return _repository.GetMenuIdsByRoleId(RoleId);
        }


        public IEnumerable<int> GetActionIdsByRoleId(int RoleId)
        {
            if (RoleId <= 0) return new int[0];

            return _repository.GetActionIdsByRoleId(RoleId);
        }

    }
}