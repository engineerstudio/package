using System.IO;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class RuntimeHelper
    {
        /// <summary>
        /// 获取当前运行时程序的路径
        /// </summary>
        public static string Path
        {
            get { return Directory.GetCurrentDirectory(); }
        }
    }
}