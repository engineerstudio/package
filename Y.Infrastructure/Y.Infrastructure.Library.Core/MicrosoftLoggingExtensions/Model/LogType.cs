using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model
{
    public enum LogType
    {
        [Description("系统错误")] SysError,
        [Description("系统日志")] SysLog,
        [Description("商户日志")] MerchantLog,
    }


    public enum MerchantLogType
    {
        [Description("常规日志")] None,
    }


    public enum SystemLogType
    {
        [Description("常规日志")] None,
    }
}