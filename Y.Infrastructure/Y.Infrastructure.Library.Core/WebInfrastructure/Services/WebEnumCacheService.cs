using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Infrastructure.Library.Core.WebInfrastructure.Services
{
    public class WebEnumCacheService
    {
        public static string GetEnumStatusJsonString(string[] assemblyName)
        {
            Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
            if (assemblyName == null & assemblyName.Count() == 0) return dic.ToJson();
            foreach (var assb in assemblyName)
            {
                Assembly assembly = Assembly.Load(assb);
                Type[] ts = assembly.GetTypes();
                foreach (Type ty in ts)
                {
                    if (ty.IsEnum)
                    {
                        var fields = ty.GetFields(BindingFlags.Static | BindingFlags.Public);
                        var dicEnum = fields.ToDictionary(t => t.Name, t =>
                        {
                            DescriptionAttribute des = (DescriptionAttribute)t.GetCustomAttribute(typeof(DescriptionAttribute));
                            return des == null ? t.Name : des.Description;
                        });
                        dic.TryAdd(ty.Name, dicEnum); // 相同key需要处理
                    }
                }
            }
            return dic.ToJson();
        }

        public static Dictionary<string, string> GetEnumStatusDic(string assemblyName, string enumName)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Assembly assembly = Assembly.Load(assemblyName);
            Type[] ts = assembly.GetTypes();
            foreach (Type ty in ts)
            {
                if (ty.IsEnum && ty.Name == enumName)
                {
                    var fields = ty.GetFields(BindingFlags.Static | BindingFlags.Public);
                    var dicEnum = fields.ToDictionary(t => t.Name, t =>
                    {
                        DescriptionAttribute des = (DescriptionAttribute)t.GetCustomAttribute(typeof(DescriptionAttribute));
                        return des == null ? t.Name : des.Description;
                    });
                }
            }
            return dic;
        }
    }
}
