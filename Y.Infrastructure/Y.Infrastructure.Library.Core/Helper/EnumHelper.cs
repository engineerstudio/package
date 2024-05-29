using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取指定枚举的 字符串/描述 的方法
        /// </summary>
        /// <typeparam name="T">枚举名字</typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeyAndDescription<T>() where T : Enum
        {
            Type type = typeof(T);
            FieldInfo[] memInfo = type.GetFields();
            var dic = new Dictionary<string, string>();
            foreach (var info in memInfo)
            {
                if (!info.IsLiteral) continue;
                DescriptionAttribute[] attrs =
                    info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                dic.Add(info.Name, attrs[0].Description);
            }

            return dic;
        }


        public static List<T> ToEnumList<T>() where T : Enum
        {
            List<T> ls = new List<T>();
            foreach (T l in Enum.GetValues(typeof(T)))
                ls.Add(l);
            return ls;
        }
    }
}