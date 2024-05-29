using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Repositories.IVips;
using Y.Packet.Services.IVips;

namespace Y.Packet.Services.Vips
{
    public class WashOrderDetailService : IWashOrderDetailService
    {
        private readonly IWashOrderDetailRepository _repository;
        private readonly IWashOrderRepository _washOrderRepository;
        public WashOrderDetailService(IWashOrderDetailRepository repository, IWashOrderRepository washOrderRepository)
        {
            _repository = repository;
            _washOrderRepository = washOrderRepository;
        }



        public async Task<(IEnumerable<WashOrderDetail>, int)> GetPageListAsync(WashOrderDetailListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1  "; // AND MerchantId={q.MerchantId}
            if (q.OrderId.HasValue && q.OrderId.Value > 0)
                conditions += $" AND OrderId={q.OrderId}";
            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (list, _repository.RecordCount(conditions, parms));
        }


        public async Task<(bool, string)> InsertAsync(int userId, decimal amount, string mark, string sourceId)
        {

            var rt = await DAsync(userId, amount, mark, sourceId);
            // 3.0 这里存在一种情况，就是该条数据的打码已经满了，那么就需要进行的步骤
            // 3.1 存入当前打码数据，设置当前打码已经完结
            // 3.2 获取下一条等待打码的数据，传入当前sourceId的剩余打码量
            while (rt.Balance < 0)
            {
                rt = await DAsync(userId, Math.Abs(rt.Balance), mark, sourceId);
            }

            // 3.判断当前用户是否还存在未完成订单,判断用户金额是否为零
            //  用户不存在未完成的订单 && 用户的余额为零, 则清空打码数据

            //// 1. 判断是否有需要打码的订单
            //string sql = $"SELECT TOP 1* FROM WashOrder WHERE MemberId={userId} AND Ended=0 ORDER BY Id ASC";
            //var order = await _washOrderRepository.GetAsync(sql);
            //if (order == null) return (false, "不需要的打码量");

            //// 2. 生成打码量详细的订单
            //// 2.1 获取当前订单的所有的打码量
            //decimal existAmount = _repository.GetExistAmount(order.Id);
            //// 2.2 获取当前用户所未完成的打码量
            //decimal needAmount = _washOrderRepository.GetUnfinishedAmount(0, userId);



            //// 2.3 减去打码量金额
            //var wd = new WashOrderDetail()
            //{
            //    MemberId = userId,
            //    OrderId = order.Id,
            //    Amount = amount,
            //    Balance = needAmount - existAmount - amount, // 所有未完成的打码量-当前订单已有打码量-当前打码量 = 剩余需要的打码量
            //    Mark = mark,
            //    SourceOrderId = sourceId,
            //    CreateTime = DateTime.UtcNow.AddHours(-8)
            //};

            //await _repository.InsertAsync(wd);

            //if (existAmount + amount > order.WashAmount)
            //    _washOrderRepository.UpdateOrderStatusToFinish(order.Id);

            return (true, $"{sourceId}洗码成功");
        }


        private async Task<(bool, string, decimal Balance)> DAsync(int userId, decimal amount, string mark, string sourceId)
        {
            // 1. 获取当前用户未完成的打码订单
            string sql_wash_order_conditions = $"WHERE MemberId={userId} AND Ended=0 ORDER BY Id ASC";
            var washOrders = await _washOrderRepository.GetListAsync(sql_wash_order_conditions);
            if (washOrders.Count() == 0)
                return (false, "不需要打码", 0);

            // 2. 获取到时间排名靠后的一笔未完成的打码订单
            var wo = washOrders.OrderBy(t => t.CreateTime).First();
            // 2.0 判断该订单是否已经存入了打码量
            if (await _repository.IsExistSourceIdAsync(userId, wo.Id, sourceId)) return (false, "已存在该打码", 0);

            // 2.1 写入打码数据，并判断是否已经完成打码. 完成则更新主表打码数据，并逐笔更新数据
            // 用户打码数据已满, 则停止更新打码量
            string sql_wash_details = $"SELECT TOP 1* FROM WashOrderDetail WHERE OrderId={wo.Id} ORDER BY Id DESC;";
            var details = await _repository.GetAsync(sql_wash_details);
            decimal balance = 0;
            if (details == null) balance = wo.WashAmount - amount;
            else balance = details.Balance - amount;

            decimal tempBalance = balance;
            if (balance < 0)
            {
                amount = details == null ? wo.WashAmount : details.Balance;
                balance = 0;
            }
            var wd = new WashOrderDetail()
            {
                MemberId = userId,
                OrderId = wo.Id,
                Amount = amount,
                Balance = balance, // 所有未完成的打码量-当前订单已有打码量-当前打码量 = 剩余需要的打码量
                Mark = mark,
                SourceOrderId = sourceId,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };
            await _repository.InsertAsync(wd);
            //wo.WashAmount += amount;
            if (balance <= 0)
            {
                await _washOrderRepository.UpdateOrderStatusToFinishAsync(wo.Id);
            }
            return (true, $"{sourceId}洗码成功", tempBalance);
        }


        public (bool, string) Insert()
        {

            throw new NotImplementedException();
        }


        // 获取当前剩余打码量
        public async Task<decimal> GetLeftWashAmount(int merchantId, int userId)
        {
            //string sql = $"SELECT ID FROM [WashOrder] WHERE MemberId={userId} Ended = 0";
            //string sql2 = "SELECT SUM(Balance) FROM [WashOrderDetail] WHERE ";

            return 0;

        }

        public async Task<decimal> GetTotalWashAmountAsync(int memberId) => await _repository.GetTotalWashAmountAsync(memberId);

    }
}