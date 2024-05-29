using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.Helper
{
    public static class DictionaryHelper
    {
        /// <summary>
        /// 枚举类型转换字典 <枚举名字，Description属性>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumTypeDicByType<T>() where T : Enum
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            Type t = typeof(T);
            FieldInfo[] fields = t.GetFields();

            foreach (FieldInfo f in fields)
            {
                if (!f.IsLiteral) continue;
                DescriptionAttribute[] attrs =
                    f.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                dics.Add(f.Name, attrs[0].Description);
            }

            return dics;
        }
    }
}