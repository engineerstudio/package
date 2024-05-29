namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    public class WebApiResultEntity
    {
        /// <summary>
        /// 返回请求状态，成功Sucess,失败Failed
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误信息代码，如果请求数据成功,则这个为空，如果请求数据异常,则显示异常代码
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public dynamic Results { get; set; }
    }


    public class WebApiResultTable
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}