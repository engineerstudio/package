using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;

namespace Y.Infrastructure.Library.EventsTriggers
{
    public class TriggerHandlerHelper
    {
        public static IEnumerable<Type> GetHandlerTypes(params Type[] ignoreHandlerAttributeTypes)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(c => c.FullName.StartsWith("SportsGame.Infrastructure"));
            //assemblies = assemblies.Where(c => c.GetCustomAttribute<MessageHanderAttribute>() != null);
            var res = assemblies.SelectMany(c => c.ExportedTypes).Where(c => c.GetCustomAttribute<MessageHanderAttribute>() != null);

            if (ignoreHandlerAttributeTypes?.Length > 0)
            {
                res = res.Where(x => ignoreHandlerAttributeTypes.All(y => x.GetCustomAttribute(y) == null));
            }

            return res;
        }

        public static async Task MessageEventArgsHandler(string channel, string message, IServiceProvider provider)
        {
            try
            {
                var dic = JsonConvert.DeserializeObject<MessageEventArgs>(message);
                // 获取当前的 EventArgs 所有缓存 进行匹配key
                var instance = EventsInstanceHelper.Instance().GetHandler($"{dic.Key}Handler", dic.Value);
                // 匹配到key的实例 执行process方法
                instance.Process(provider);
                await instance.ProcessAsync(provider);
            }
            catch (Exception ex)
            {
                IExcptLogsService excptLogsServices = (IExcptLogsService)provider.GetService(typeof(IExcptLogsService));
                excptLogsServices.Insert(ex, " SportsGame.Infrastructure.EventsTriggers");
            }
            finally { }
        }

    }
}
