namespace Y.Infrastructure.Library.Core.ViewModel.LayuiTable
{
    public class TableDataModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = 0;

        /// <summary>
        /// 操作消息
        /// </summary>
        public string msg { get; set; } = "操作成功";

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public dynamic data { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public dynamic totalRow { get; set; }
    }
}