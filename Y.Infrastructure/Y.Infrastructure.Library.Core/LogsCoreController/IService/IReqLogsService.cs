
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Core.LogsCoreController.IService
{
    public interface IReqLogsService
    {
        /// <summary>
        /// 保存请求数据
        /// </summary>
        /// <param name="merchantId">商户Id</param>
        /// <param name="url">链接地址</param>
        /// <param name="content">请求内容</param>
        void Insert(int merchantId, string url, string content);
        Task InsertAsync(int merchantId, string url, string content);
    }
}