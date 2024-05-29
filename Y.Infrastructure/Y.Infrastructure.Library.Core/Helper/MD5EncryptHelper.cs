using System.Security.Cryptography;
using System.Text;

namespace Y.Infrastructure.Library.Core.Encrypt
{
    public static class MD5EncryptHelper
    {
        /// <summary>
        /// 返回MD5加密字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="st">加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位</param>
        /// <returns></returns>
        public static string ToMD5(string source, string st = null)
        {
            if (st == null) st = "x2"; // 默认返回32位加密么

            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(sor);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString(st));
            }

            return sb.ToString();
        }
    }
}