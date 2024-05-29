using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.MultiLanguage.Services;
using Y.Infrastructure.Library.Core.MultiLanguageController.Enums;
using Y.Infrastructure.Library.Core.MultiLanguageController.ViewModels;

namespace Y.Infrastructure.Library.Core.MultiLanguageController
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class MultiLanguageBaseController : ControllerBase
    {
        private readonly string MerchantId;
        private readonly IDocsService _docsService;
        public MultiLanguageBaseController(string merchantId, IDocsService docsService)
        {
            _docsService = docsService;
        }

        [HttpPost("create")]
        public async Task<string> CreateAsync([FromBody] MultiLanguageInsertViewModel vm)
        {
            var dic = new Dictionary<LanguageType, string>();
            if (!vm.Zhcn.IsNullOrEmpty())
                dic.Add(LanguageType.ZHCN, vm.Zhcn);
            if (!vm.Zhtw.IsNullOrEmpty())
                dic.Add(LanguageType.ZHTW, vm.Zhtw);
            if (!vm.En.IsNullOrEmpty())
                dic.Add(LanguageType.EN, vm.En);

            var rt = await _docsService.CreateAsync(MerchantId, dic);
            return rt.ToJsonResult();
        }


        public async Task<string> UpdateAsync([FromBody] MultiLanguageUpdateViewModel vm)
        {
            var rt = await _docsService.UpdateTypeByIdAsync(MerchantId, vm.Id, vm.Type, vm.Value);
            return rt.ToJsonResult();
        }


    }
}
