using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.MultiLanguageController.Enums;

namespace Y.Infrastructure.Library.Core.MultiLanguage.Services
{
    public interface IDocsService
    {
        Task<(bool, string)> CreateAsync(string merchantId, Dictionary<LanguageType, string> dicData);
        Task<(bool, string)> UpdateTypeByIdAsync(string merchantId, int id, LanguageType type, string value);
        Task<(bool, string)> DeleteByIdAsync(string merchantId, int id);
    }
}