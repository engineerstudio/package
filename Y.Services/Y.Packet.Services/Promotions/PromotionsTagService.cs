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
*│　创建时间：2020-07-26 00:31:01                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Promotions                                  
*│　类    名： PromotionsTypeService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Mapper;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Services.IPromotions;

namespace Y.Packet.Services.Promotions
{
    public class PromotionsTagService : IPromotionsTagService
    {
        private readonly IPromotionsTagRepository _repository;

        public PromotionsTagService(IPromotionsTagRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<PromotionsTag>, int)> GetPageListAsync(PromotionsTagListPageQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $" WHERE MerchantId=@MerchantId ";
            parms.Add("MerchantId", q.MerchantId);
            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms), await _repository.RecordCountAsync(conditions, parms));
        }


        public async Task<(bool, string)> InsertOrModifyAsync(PromotionsTagAddOrModifyModel m)
        {
            if (m.Name.IsNullOrEmpty()) return (false, "标签名称不能为空");
            int? rt = null;
            if (m.Id == 0)
            {
                var entity = ModelToEntity.Mapping<PromotionsTag, PromotionsTagAddOrModifyModel>(m);
                entity.CreateTime = DateTime.UtcNow.AddHours(8);
                rt = await _repository.InsertWithCacheAsync(entity);
            }
            else
            {
                var entity = await _repository.GetFromCacheAsync(m.MerchantId, m.Id);
                //if (entity.MerchantId != m.MerchantId) return (false, "商户错误");
                entity.Name = m.Name;
                entity.Sort = m.Sort;
                entity.Image = m.Image;
                rt = await _repository.UpdateWithCacheAsync(entity);
            }
            if (rt == null || rt.Value < 1) return (false, "保存失败");

            return (true, "保存成功");
        }


        public async Task<(bool, string)> DeleteAsync(int merchantId, int tagId)
        {
            if (merchantId == 0 || tagId == 0) return (false, "参数错误");
            var rt = await _repository.DeleteAsync(merchantId, tagId);
            if (rt > 0) return (true, "删除成功");
            return (false, "删除失败");
        }


        public async Task<(bool, Dictionary<int, string>)> GetDicAsync(int merchantId)
        {
            if (merchantId == 0) return (false, new Dictionary<int, string>());
            return (true, (await _repository.GetListAsync(merchantId)).ToDictionary(t => t.Id, t => t.Name));
        }

        public async Task<(bool, IEnumerable<PromotionsTag>)> GetListsAsync(int merchantId)
        {
            if (merchantId == 0) return (false, null);
            return (true, await _repository.GetListAsync(merchantId));
        }
    }
}