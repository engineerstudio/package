namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class NumberExtensions
    {
        /// <summary>
        /// int类型数字是否是奇数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEvenNumber(this int value)
        {
            return value % 2 != 0;
        }
    }
}