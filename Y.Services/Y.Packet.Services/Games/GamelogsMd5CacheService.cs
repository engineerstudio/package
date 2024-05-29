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
*│　创建时间：2020-09-13 23:23:32                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GamelogsMd5CacheService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Packet.Entities.Games;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GamelogsMd5CacheService : IGamelogsMd5CacheService
    {
        private readonly IGamelogsMd5CacheRepository _repository;

        public GamelogsMd5CacheService(IGamelogsMd5CacheRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 判断MD5是否存在, 如果不存在则插入该数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sourceId"></param>
        /// <param name="md5"></param>
        /// <returns>bool1:是否存在sourceId的数据，bool2:MD5是否一致</returns>
        public async Task<(bool, bool)> Process(string typeStr, string sourceId, string md5)
        {
            //string sql = $"SELECT * FROM GamelogsMd5Cache WHERE GameTypeStr='{typeStr}' AND SourceId='{sourceId}'";
            var entity = await _repository.GetFromCacheAsync(typeStr, sourceId);
            string md5Str = MD5EncryptHelper.ToMD5(md5);
            if (entity == null)
            {
                //var m = new GamelogsMd5Cache();
                //m.SourceId = sourceId;
                //m.RawMd5 = md5Str;
                //m.GameTypeStr = typeStr;
                //var rt = await _repository.InsertWithCacheAsync(m);
                return (false, false);
            }
            if (entity.RawMd5 == md5Str) return (true, true); // 数据未更新
            else return (true, false);  // 存在sourceId的数据，并且md5不一致，那么更新数据
        }

        public async Task InsertAsync(string typeStr, string sourceId, string md5)
        {
            string md5Str = MD5EncryptHelper.ToMD5(md5);
            var m = new GamelogsMd5Cache();
            m.SourceId = sourceId;
            m.RawMd5 = md5Str;
            m.GameTypeStr = typeStr;
            var rt = await _repository.InsertWithCacheAsync(m);
        }



    }
}