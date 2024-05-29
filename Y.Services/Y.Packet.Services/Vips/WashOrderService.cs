using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Repositories.IVips;
using Y.Packet.Services.IVips;

namespace Y.Packet.Services.Vips
{
    public class WashOrderService : IWashOrderService
    {
        private readonly IWashOrderRepository _repository;
        private readonly IWashOrderDetailRepository _washOrderDetailRepository;
        public WashOrderService(IWashOrderRepository repository, IWashOrderDetailRepository washOrderDetailRepository)
        {
            _repository = repository;
            _washOrderDetailRepository = washOrderDetailRepository;
        }

        public async Task<(IEnumerable<WashOrder>, int)> GetPageListAsync(WashOrderListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 "; // AND MerchantId={q.MerchantId} 

            if (q.MemberId.HasValue && q.MemberId.Value > 0)
                conditions += $" AND MemberId={q.MemberId}";

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (list, _repository.RecordCount(conditions, parms));
        }

        /// <summary>
        /// 清理当前待洗码订单
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> ClearWashOrderAsync(int memberId)
        {
            string sql_wash_order_conditions = $"WHERE MemberId={memberId} AND Ended = 0 ORDER BY Id ASC";
            var washOrders = await _repository.GetListAsync(sql_wash_order_conditions);
            if (washOrders.Count() == 0)
                return (false, "不需要打码");
            foreach (var wo in washOrders)
            {
                wo.Ended = true;
                wo.Mark = "用户余额清零";
                await _repository.UpdateAsync(wo);
            }
            return (true,string.Empty);
        }



        public async Task<(bool, string)> InsertAsync(int memberId, FundLogType fundsType, decimal amount, decimal washAmount, string mark)
        {
            if (amount <= 0 || washAmount <= 0) return (false, "打码金额不能小于等于零");
            if (mark.IsNullOrEmpty()) mark = "";

            var order = new WashOrder()
            {
                MemberId = memberId,
                FundsType = fundsType.ToString(),
                Amount = amount,
                WashAmount = washAmount,
                Mark = mark,
                CreateTime = DateTime.UtcNow.AddHours(8),
                Ended = false
            };

            var rt = await _repository.InsertAsync(order);

            if (rt != null && rt.Value == 0) return (false, "保存失败");

            var wd = new WashOrderDetail()
            {
                MemberId = memberId,
                OrderId = rt.Value,
                Amount = 0,
                Balance = washAmount, // 所有未完成的打码量-当前订单已有打码量-当前打码量 = 剩余需要的打码量
                Mark = mark,
                SourceOrderId = $"WashId：{rt.Value}",
                CreateTime = DateTime.UtcNow.AddHours(8)
            };
            await _washOrderDetailRepository.InsertAsync(wd); 

            return (true, "保存成功");

        }




    }
}