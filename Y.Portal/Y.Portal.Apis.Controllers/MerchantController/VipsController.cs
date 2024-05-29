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
    public class VipsController : ControllerBase
    {
        private readonly IVipGroupsService _vipGroupsService;
        private readonly IBaseHandlerService _baseHandler;
        public VipsController(IBaseHandlerService baseHandler, IVipGroupsService vipGroupsService)
        {
            _vipGroupsService = vipGroupsService;
            _baseHandler = baseHandler;
        }


        /// <summary>
        /// VIP列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("load")]
        public async Task<string> GetPageListAsync([FromForm] VipListQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            var result = await _vipGroupsService.GetPageListAsync(q);
            return result.ToTableModel<VipGroups>().ToJson();
        }

        /// <summary>
        /// 添加VPI信息
        /// </summary>
        /// <param name="rmodel"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<string> AddVipGroup([FromForm] VipGroupModel rmodel)
        {
            if (rmodel.MerchantId == 0) rmodel.MerchantId = _baseHandler.MerchantId;

            var rt = await _vipGroupsService.InsertOrUpdateAsync(rmodel);
            return rt.ToJsonResult();
        }

        /// <summary>
        /// 设置分组规则
        /// </summary>
        /// <param name="rmodel"></param>
        /// <returns></returns>
        [HttpPost("setgroup")]
        public async Task<string> SetGroupRule([FromForm] GroupSetting rmodel)
        {
            int merchantId = _baseHandler.MerchantId;

            var rt = await _vipGroupsService.SetGroupRuleAsync(rmodel, merchantId, rmodel.GroupId);
            return rt.ToJsonResult();
        }

        /// <summary>
        /// 获取分组字典 ID/NAME
        /// </summary>
        /// <returns></returns>
        [HttpPost("groupdic")]
        public async Task<string> GetGroupDic()
        {
            int merchantId = _baseHandler.MerchantId;
            var rt = await _vipGroupsService.GetGroupIdAndNameDicAsync(merchantId);
            return (rt.Item1, rt.Item2.ToJson()).ToJsonResult();
        }


        /// <summary>
        /// 设置分组的可用支付渠道
        /// </summary>
        /// <param name="id"></param>
        /// <param name="merchantPayIds"></param>
        /// <returns></returns>
        [HttpPost("setpay")]
        public async Task<string> SetPayRule([FromForm] int id, [FromForm] string merchantPayIds)
        {
            int merchantId = _baseHandler.MerchantId;

            var rt = await _vipGroupsService.SetPayRuleAsync(merchantId, id, merchantPayIds);
            return rt.ToJsonResult();
        }




    }
}
