using System;
using System.Collections;

namespace Y.Infrastructure.Library.Core.Helper
{
    public static class ExceptionsHelper
    {
        /// <summary>
        /// 提取异常及其内部异常堆栈跟踪
        /// </summary>
        /// <param name="exception">提取的例外</param>
        /// <param name="lastStackTrace">最后提取的堆栈跟踪（对于递归）， String.Empty or null</param>
        /// <param name="exCount">提取的堆栈数（对于递归）</param>
        /// <returns>Syste.String</returns>
        public static string ExtractAllStackTrace(this Exception exception, string lastStackTrace = null,
            int exCount = 1)
        {
            var ex = exception;
            const string entryFormat = "#{0}: {1}\r\n{2}";
            //修复最后一个堆栈跟踪参数
            lastStackTrace = lastStackTrace ?? string.Empty;
            //添加异常的堆栈跟踪
            lastStackTrace += string.Format(entryFormat, exCount, ex.Message, ex.StackTrace);
            if (exception.Data.Count > 0)
            {
                lastStackTrace += "\r\n    Data: ";
                foreach (var item in exception.Data)
                {
                    var entry = (DictionaryEntry) item;
                    lastStackTrace += string.Format("\r\n\t{0}: {1}", entry.Key, exception.Data[entry.Key]);
                }
            }

            //递归添加内部异常
            if ((ex = ex.InnerException) != null)
                return ex.ExtractAllStackTrace(string.Format("{0}\r\n\r\n", lastStackTrace), ++exCount);
            return lastStackTrace;
        }
        //private string ErrorMessage(Exception exception)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.AppendLine("====================EXCEPTION====================");
        //    stringBuilder.AppendLine("【Message】:" + exception.Message);
        //    stringBuilder.AppendLine("【Source】:" + exception.Source);
        //    stringBuilder.AppendLine("【TargetSite】:" + ((exception.TargetSite != null) ? exception.TargetSite.Name : "无"));
        //    stringBuilder.AppendLine("【StackTrace】:" + exception.StackTrace);
        //    stringBuilder.AppendLine("【exception】:" + exception);
        //    stringBuilder.AppendLine("=================================================");
        //    if (exception.InnerException != null)
        //    {
        //        stringBuilder.AppendLine("====================INNER EXCEPTION====================");
        //        stringBuilder.AppendLine("【Message】:" + exception.InnerException.Message);
        //        stringBuilder.AppendLine("【Source】:" + exception.InnerException.Source);
        //        stringBuilder.AppendLine("【TargetSite】:" + ((exception.InnerException.TargetSite != null) ? exception.InnerException.TargetSite.Name : "无"));
        //        stringBuilder.AppendLine("【StackTrace】:" + exception.InnerException.StackTrace);
        //        stringBuilder.AppendLine(("【InnerException】:" + exception.InnerException) ?? "");
        //        stringBuilder.AppendLine("=================================================");
        //    }
        //    return stringBuilder.ToString().Replace("/r", "").Replace("/n", "");
        //}
    }
}