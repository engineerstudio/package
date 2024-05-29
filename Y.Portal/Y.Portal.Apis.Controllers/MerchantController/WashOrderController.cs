using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IPromotions;
using Y.Packet.Services.IVips;
using Y.Portal.Apis.Controllers.DtoModel.Merchant;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class WashOrderController : ControllerBase
    {
        private readonly IWashOrderService _washOrderService;
        private readonly IWashOrderDetailService _washOrderDetailService;
        private readonly IBaseHandlerService _baseHandlerService;
        public WashOrderController(IWashOrderService washOrderService, IWashOrderDetailService washOrderDetailService, IBaseHandlerService baseHandlerService)
        {
            _washOrderService = washOrderService;
            _washOrderDetailService = washOrderDetailService;
            _baseHandlerService = baseHandlerService;
        }

        [HttpPost("washorderload")]
        public async Task<string> WashOrderListAsync([FromForm] WashOrderListQuery q)
        {
            q.MerchantId = _baseHandlerService.MerchantId;
            var rt = await _washOrderService.GetPageListAsync(q);
            return (new TableDataModel()
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    FundsType = t.FundsType,//.GetDescription(),
                    t.Amount,
                    t.WashAmount,
                    t.Mark,
                    t.CreateTime,
                    Ended = t.Ended ? "已完结" : "未完结",
                })
            }).ToJson();
        }

        [HttpPost("washorderdetailsload")]
        public async Task<string> WashOrderDetailsListAsync([FromForm] WashOrderDetailListQuery q)
        {
            q.MerchantId = _baseHandlerService.MerchantId;
            var rt = await _washOrderDetailService.GetPageListAsync(q);

            return (new TableDataModel()
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.Balance,
                    t.Amount,
                    t.Mark,
                    t.CreateTime,
                    t.SourceOrderId
                })
            }).ToJson();
        }

    }
}
