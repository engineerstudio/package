using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Mapper;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;
using static Y.Packet.Entities.Merchants.Domains;

namespace Y.Packet.Services.Merchants
{
    public class DomiansService : IDomiansService
    {
        private readonly IDomiansRepository _repository;
        public DomiansService(IDomiansRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool, string)> InsertOrModifyAsync(DomiansInsertOrEditViewModel domain)
        {
            if (string.IsNullOrEmpty(domain.Name)) return (false, "请输入域名");
            if (!XValidator.IsUrl2(domain.Name)) return (false, "请输入正确的域名格式");
            if (string.IsNullOrEmpty(domain.Marks)) domain.Marks = "";
            // 判断域名是否已经存在
            if (domain.Id == 0 && (await _repository.ExistDomainNoAsync(domain.Name)) > 0) return (false, "域名已经存在");
            Domains d = ModelToEntity.Mapping<Domains, DomiansInsertOrEditViewModel>(domain);
            int? rt;
            if (domain.Id == 0)
            {
                rt = await _repository.InsertWithCacheAsync(d);
            }
            else
            {
                rt = await _repository.UpdateWithCacheAsync(d);
            }
            if (rt != null && rt.Value > 0) return (true, "保存成功");
            return (false, "保存失败");
        }

        public (IEnumerable<Domains>, int) GetPageList(DomiansListQuery q)
        {
            string conditions = "WHERE 1=1 ";
            if (q.MerchantId != 0)
                conditions += $" AND MerchantId={q.MerchantId} ";
            if (!q.DType.IsNullOrEmpty())
            {
                var type = q.DType.ToEnum<DomainsType>();
                conditions += $" AND DoType={type.GetEnumValue()} ";
            }
            return (_repository.GetListPaged(q.Page, q.Limit, conditions, "Id desc", null), _repository.RecordCount());
        }

        public async Task<(bool, string, int)> ExistLikeAsync(string url)
        {
            if (string.IsNullOrEmpty(url)) return (false, "地址不存在", 0);

            IEnumerable<Domains> rt = null;
            var merchantId = await _repository.GetMerchantIdByDomainAsync(url);
            if (merchantId == 0)
                rt = await _repository.GetByLikeUrlAsync(url);
            else
                return (true, null, merchantId);

            if (rt == null || rt.Count() == 0) return (false, "", 0);

            return (true, "", rt.First().MerchantId);
        }

        public async Task<string> GetCallbackUrlAsync(int merchantId)
        {
            var link = (await _repository.GetCallBackUrlCacheAsync(merchantId));
            if (!link.IsNullOrEmpty()) return $"https://{link}";

            string sql = $"SELECT TOP 1* FROM Domains WHERE MerchantId={merchantId} ORDER BY ID ASC";
            var m = await _repository.GetAsync(sql);
            return $"{(m.IsHttps ? "https://" : "http://")}{m.Name}";
        }

        public async Task<(bool, string)> DelAsync(int id)
        {
            if (id == 0) return (false, $"数据不存在{id}");
            var rt = await _repository.DeleteCacheAsync(id);
            return rt.ToResult("删除成功", "删除失败");
        }
    }
}