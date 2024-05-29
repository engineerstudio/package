using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举对应的值
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int GetEnumValue(this object enumValue)
        {
            return (int) enumValue;
        }

        public static int GetEnumValue<T>(this object enumValue) where T : struct
        {
            var e = enumValue.ToString().ToEnum<T>().Value;
            return (int) enumValue;
        }

        /// <summary>
        /// 获取特性 (DisplayAttribute) 的名称；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Name : enumValue.ToString();
        }

        /// <summary>
        /// 获取特性 (DisplayAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayDescription(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 获取特性 (DescriptionAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 直接获取特性（更轻量、更容易使用，不用封装“获取每一个自定义特性”的扩展方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            Type type = enumValue.GetType();
            MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T) attributes[0] : null;
        }


        public static bool ExistAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            Type type = enumValue.GetType();
            MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0;
        }


        /// <summary>
        /// 获取指定类型枚举的键值对数据 键:枚举名称，值：描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static bool GetEnumTypeDicByType<T>() where T : Enum
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

            return true;
        }


        /// <summary>
        /// 获取值-枚举 int/string 字符串字典
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumTypeDic(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (int vl in Enum.GetValues(type))
                dic.Add(vl, Enum.GetName(type, vl));
            return dic;
        }


        public static Dictionary<int, T> GetEnumTypeToValueTDic<T>(this Enum enumValue) where T : notnull, Enum
        {
            Type type = enumValue.GetType();
            Dictionary<int, T> dic = new Dictionary<int, T>();
            foreach (int vl in Enum.GetValues(type))
            {
                T enumType = (T)Enum.ToObject(typeof(T), vl);
                dic.Add(vl, enumType);
            }
            return dic;
        }


        public static Dictionary<int, string> GetEnumTypeToDescValueTDic<T>(this Enum enumValue) where T : notnull, Enum
        {
            Type type = enumValue.GetType();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (int vl in Enum.GetValues(type))
            {
                T enumType = (T)Enum.ToObject(typeof(T), vl);
                dic.Add(vl, enumType.GetDescription());
            }
            return dic;
        }
    }
}