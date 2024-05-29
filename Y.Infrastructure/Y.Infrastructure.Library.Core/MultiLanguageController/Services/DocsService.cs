using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.MultiLanguage.Db;
using Y.Infrastructure.Library.Core.MultiLanguage.Repositories;
using Y.Infrastructure.Library.Core.MultiLanguageController.Enums;

namespace Y.Infrastructure.Library.Core.MultiLanguage.Services
{
    public class DocsService : IDocsService
    {
        private readonly IDocsRepository _repository;

        public DocsService(IDocsRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool, string)> CreateAsync(string merchantId, Dictionary<LanguageType, string> dicData)
        {
            if (merchantId.IsNullOrEmpty()) return (false, "未查询到站点信息");
            var docs = new Docs();
            docs.MerchantId = merchantId;
            foreach (var dic in dicData)
            {
                switch (dic.Key)
                {
                    case LanguageType.ZHCN:
                        docs.Zhcn = dic.Value;
                        break;
                    case LanguageType.ZHTW:
                        docs.Zhtw = dic.Value;
                        break;
                    case LanguageType.EN:
                        docs.En = dic.Value;
                        break;
                    default:
                        break;
                }
            }
            var rt = await _repository.InsertAsync(docs);
            return rt.ToResult("保存成功", "保存失败");
        }

        public Task<(bool, string)> DeleteByIdAsync(string merchantId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)> UpdateTypeByIdAsync(string merchantId, int id, LanguageType type, string value)
        {
            var docs = await _repository.GetAsync(id);
            if (docs == null) return (false, "未查询到");
            if (docs.MerchantId != merchantId) return (false, "未查询到");
            switch (type)
            {
                case LanguageType.ZHCN:
                    docs.Zhcn = value;
                    break;
                case LanguageType.ZHTW:
                    docs.Zhtw = value;
                    break;
                case LanguageType.EN:
                    docs.En = value;
                    break;
                default:
                    break;
            }
            var rt = await _repository.UpdateAsync(docs);
            return rt.ToResult("保存成功", "保存失败");
        }
    }
}