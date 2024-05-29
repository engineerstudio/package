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
*│　描    述：用户游戏信息表                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-09-07 16:30:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GameUsersService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameUsersService : IGameUsersService
    {
        private readonly IGameUsersRepository _repository;
        private readonly IReqLogsService _reqLogsService;

        public GameUsersService(IGameUsersRepository repository, IServiceProvider provider)
        {
            _repository = repository;
            _reqLogsService = (IReqLogsService)provider.GetService(typeof(IReqLogsService));
        }

        public async Task<(bool exist, string gameUserName)> ExistAsync(int MemberId, GameType gameType)
        {
            //var player = await _repository.GetAsync($"SELECT * FROM GameUsers WHERE MemberId={MemberId} AND TypeStr='{gameType.ToString()}'");
            //if (player == null) return (false, string.Empty);
            var playerName = await _repository.GetPlayerNameAsync(MemberId, gameType);
            return (playerName != null, playerName);
        }


        public async Task<(bool, string)> DeleteGameUserAsync(int memberId, GameType gameType)
        {
            var rt = await _repository.DeleteAsync(memberId, gameType);
            return rt.ToResult("删除成功", "删除失败");
        }

        public async Task<string> GetPlayerNameAsync(int MemberId, GameType gameType)
        {
            //var player = await _repository.GetAsync($"SELECT * FROM GameUsers WHERE MemberId={MemberId} AND TypeStr='{gameType.ToString()}'");
            var playerName = await _repository.GetPlayerNameAsync(MemberId, gameType);
            if (playerName == null) return null;
            return playerName;
        }

        //public async Task<GameUsers> GetByGameTypeStrAsync(string gameTypeStr, int memberId)
        //{
        //    // TODO 写入缓存表
        //    var player = await _repository.GetAsync($"SELECT * FROM GameUsers WHERE MemberId={memberId} AND TypeStr='{gameTypeStr}'");
        //    return player;
        //}


        public async Task<(bool, string)> SaveGameUserAsync(int merchantId, int memberId, string gameStr, string playerName, string psw)
        {
            if (memberId == 0 || merchantId == 0 || string.IsNullOrEmpty(playerName)) return (false, "参数错误");
            var m = new GameUsers();
            m.TypeStr = gameStr;
            m.MemberId = memberId;
            m.MerchantId = merchantId;
            m.PlayerName = playerName;
            m.PlayerPsw = psw == null ? "" : psw;
            m.Balance = 0;
            m.IsLock = false;
            m.IsTest = false;
            m.RegisterTime = DateTime.UtcNow.AddHours(8);
            m.LastLoginTime = DefaultString.DefaultDateTime;
            var rt = await _repository.InsertWithCacheAsync(m);
            return rt.ToResult("保存成功", "保存失败");
        }


        public async Task<(bool, int, int)> GetByPlayerNameAsync(string gameStr, string playerName)
        {
            // TODO 根据游戏类别与playerName添加缓存  这里的数据从缓存中获取
            //SELECT * FROM GameUsers
            //var player = await _repository.GetAsync($"SELECT * FROM GameUsers WHERE TypeStr='{gameStr}' AND PlayerName='{playerName}'");
            //if (player == null) return (false, 0, 0);

            //var player = await _repository.GetByPlayerNameAsync(gameStr, playerName);
            var d = await _repository.GetByMerchantIdAndMemberIdAsync(gameStr, playerName);
            return (true, d.MerchantId, d.MemberId);
        }


        public async Task<IEnumerable<GameUsers>> GetAsync(int merchantId, int memberId)
        {
            if (merchantId == 0) return null;
            //string sql = $"WHERE MerchantId={merchantId} AND MemberId={memberId}";
            //return await _repository.GetListAsync(sql, null);
            return await _repository.GetListAsync(merchantId, memberId);
        }

    }
}