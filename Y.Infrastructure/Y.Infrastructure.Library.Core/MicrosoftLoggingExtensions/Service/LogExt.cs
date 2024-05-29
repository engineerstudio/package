using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model;
using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Repository;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions
{
    public class LogExt
    {
        private volatile static LogExt _instance;

        private readonly static object obj = new object();

        public LogExt()
        {
        }

        public static LogExt Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (obj)
                    {
                        _instance = new LogExt();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="logger">记录人/类型</param>
        /// <param name="msg">记录信息</param>
        /// <param name="mark">备注信息</param>
        public void AddDebugLog(string logger, string msg, string mark = null)
        {
            var log = new DebugLog()
            {
                Logger = logger,
                Message = msg,
                Mark = mark
            };
            var rep = new DebugLogRepository(null);

            rep.Insert(log);
        }
    }
}