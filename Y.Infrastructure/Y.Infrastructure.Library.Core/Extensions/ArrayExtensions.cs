using System;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// string[] 转 int[]
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this string[] arr)
        {
            return Array.ConvertAll<string, int>(arr, t => int.Parse(t));
        }

        /// <summary>
        /// string[] 转 逗号分隔数组 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ToCommaSplitString(this string[] arr)
        {
            return string.Join(',', arr);
        }
        public static string ToCommaSplitString2(this string[] arr)
        {
            string str = string.Empty;
            foreach (var a in arr)
            {
                str += $"'{a}',";
            }
            return str.RemoveLastChar();
        }
    }
}