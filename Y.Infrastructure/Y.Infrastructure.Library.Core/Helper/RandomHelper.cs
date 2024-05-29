using System;
using System.Text;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class RandomHelper
    {
        /// <summary>
        /// 获取指定位数的数字随机数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetNumber(int? count)
        {
            int a = 0, b = 9;
            switch (count)
            {
                case 1:
                    a = 0;
                    b = 9;
                    break;
                case 2:
                    a = 10;
                    b = 99;
                    break;
                case 3:
                    a = 100;
                    b = 999;
                    break;
                case 4:
                    a = 1000;
                    b = 9999;
                    break;
                case 5:
                    a = 10000;
                    b = 99999;
                    break;
                default:
                    break;
            }

            Random ran = new Random();
            int num = ran.Next(a, b);
            return num.ToString();
        }

        public static long GetNumber()
        {
            return DateTime.Now.Millisecond;
        }


        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetString(int? count)
        {
            //A-Z的 ASCII值为65-90
            if (count == 0) count = 1;
            return Guid.NewGuid().ToString("N").Substring(0, count.Value);
        }

        /// <summary>
        /// 获取Guid
        /// 38bddf48f43c48588e0d78761eaa1ce6
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }


        public static string GetCapitalString(int count)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(65, 91);
            return Convert.ToChar(num).ToString().Substring(0, count);
        }


        /// <summary>
        /// 根据GUID获取16位的唯一字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int) b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        /// <returns></returns>
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 生成22位唯一的数字 并发可用 
        /// </summary>
        /// <returns></returns>
        public static string GenerateUniqueID()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一  
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(1000, 9999);
            return strUnique;
        }


        /// <summary>
        /// 生成单个随机数字
        /// </summary>
        /// <returns></returns>
        public static int CreateNum()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(10);
            return num;
        }

        /// <summary>
        /// 生成单个大写随机字母
        /// </summary>
        private string createBigAbc()
        {
            //A-Z的 ASCII值为65-90
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(65, 91);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }

        /// <summary>
        /// 生成单个小写随机字母
        /// </summary>
        private static string GetLowerCase()
        {
            //a-z的 ASCII值为97-122
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(97, 123);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }

        public static string GetLowerCase(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
                sb.Append(GetLowerCase());
            return sb.ToString();
        }
    }
}