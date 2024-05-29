namespace Y.Infrastructure.Library.Core.ViewModel
{
    public class BaseResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }

        public BaseResult()
        {
        }

        public BaseResult(int resultCode, string resultMsg)
        {
            code = resultCode;
            msg = resultMsg;
        }

        /// <summary>
        /// 集合数据
        /// </summary>
        public dynamic data { get; set; }

        /// <summary>
        /// 单条数据
        /// </summary>
        public object info { get; set; }
    }

    public class TableResult : BaseResult
    {
        public TableResult()
        {
            Page = 0;
            Limit = 10;
        }

        public int Page { get; set; }
        public int Limit { get; set; }
    }
}