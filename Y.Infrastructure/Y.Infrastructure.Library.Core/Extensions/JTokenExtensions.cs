using Newtonsoft.Json.Linq;
using System;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class JTokenExtensions
    {
        public static dynamic ValueEx<U>(this JToken value, int time = 0)
        {
            if (typeof(U) == typeof(string))
            {
                if (value == null) return "";
                return value.ToString();
            }
            else if (typeof(U) == typeof(DateTime))
            {
                if (value == null) return Convert.ToDateTime("1900-01-01");
                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);
                if (dt != default(DateTime))
                    return dt.AddHours(time);
                return Convert.ToDateTime("1900-01-01");
            }
            else if (typeof(U) == typeof(Decimal))
            {
                if (value == null) return 0;
                decimal d;
                Decimal.TryParse(value.ToString(), out d);
                return d;
            }
            else if (typeof(U) == typeof(int))
            {
                if (value == null) return 0;
                int d;
                int.TryParse(value.ToString(), out d);
                return d;
            }
            else if (typeof(U) == typeof(long))
            {
                if (value == null) return 0;
                long d;
                long.TryParse(value.ToString(), out d);
                return d;
            }


            return "";
        }
    }
}