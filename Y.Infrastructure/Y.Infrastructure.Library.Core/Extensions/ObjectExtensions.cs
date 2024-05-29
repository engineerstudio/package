using System;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 格式化数据为key1=value&key2=value2的形式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="except"></param>
        /// <returns></returns>
        public static string ToFormValue(this object obj, string except = null)
        {
            string content = string.Empty;
            PropertyInfo[] pInfo = obj.GetType().GetProperties();
            foreach (var p in pInfo)
            {
                if (!string.IsNullOrEmpty(except) && p.Name == except) continue;
                content += $"{p.Name}={p.GetValue(obj)}&";
            }

            return content.Remove(content.Length - 1, 1);
        }


        public static dynamic To<U>(this object value)
        {
            switch (typeof(U).Name) //
            {
                case "Int32":
                    if (value == null) return default(int);
                    return int.Parse(value.ToString());
                case "Int64":
                    if (value == null) return default(long);
                    return Convert.ToInt64(value);
                case "Double":
                    if (value == null) return default(double);
                    return double.Parse(value.ToString());
                case "String":
                    if (value == null) return "";
                    return value.ToString();
                case "Decimal":
                    if (value == null) return default(Decimal);
                    decimal d;
                    Decimal.TryParse(value.ToString(), out d);
                    return d;
                case "DateTime":
                    if (value == null) return Convert.ToDateTime("1900-01-01");
                    DateTime dt;
                    DateTime.TryParse(value.ToString(), out dt);
                    if (dt != default(DateTime))
                        return dt;
                    return Convert.ToDateTime("1900-01-01");
                case "Boolean":

                default:
                    break;
            }

            return "";
        }

        public static bool IsEqualNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotEqualNull(this object value)
        {
            return value != null;
        }

    }
}