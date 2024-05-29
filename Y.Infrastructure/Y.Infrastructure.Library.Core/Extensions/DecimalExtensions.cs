using System.Globalization;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// 最后一位数字是否是偶数
        /// </summary>
        /// <param name="value"></param>
        /// <returns>偶数为true</returns>
        public static bool IsLastNoEven(this decimal d)
        {
            int num = GetDecimalLastNo(d);
            if (num % 2 == 0) return true;
            return false;
        }

        /// <summary>
        /// 是否大于等于5
        /// </summary>
        /// <param name="value"></param>
        /// <returns>大于5为true</returns>
        public static bool IsLastNoGreaterThan5(this decimal d)
        {
            int num = GetDecimalLastNo(d);
            if (num > 4) return true;
            return false;
        }

        /// <summary>
        /// 获取最后一位数字
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int GetDecimalLastNo(this decimal d)
        {
            string value = d.ToString("0.00");
            return value.Substring(value.Length - 1, 1).To<int>();
        }

        public static string ToCnCurrencyString(this decimal d)
        {
            CultureInfo cn = CultureInfo.GetCultureInfoByIetfLanguageTag("zh-CN");
            return d.ToString("C", cn);
        }

    }
}