using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class HttpQueryHelper
    {
        /// <summary>
        /// 请求数据, 返回Json字符串
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="strPostdata"></param>
        /// <param name="strEncoding"></param>
        /// <returns></returns>
        public static bool OpenRead(string URL, string strPostdata, Encoding encodingType, out string content,
            out string errormsg)
        {
            content = string.Empty;
            errormsg = string.Empty;
            bool call_status = false;
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);

            call_status = Post(request, encodingType, out content, out errormsg);
            return call_status;

            //try
            //{
            //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //    {
            //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), encodingType))
            //        {
            //            call_status = true;
            //            content = reader.ReadToEnd();
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    errormsg = ex.Message;
            //}
            //return call_status;
        }

        public static bool OpenRead(string URL, string strPostdata, Encoding encodingType, Dictionary<string, string> headers, out string content, out string errormsg)
        {
            content = string.Empty;
            errormsg = string.Empty;
            bool call_status = false;
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, application/json, */*";
            foreach (var h in headers)
                request.Headers.Add(h.Key, h.Value);

            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            //request.GetRequestStream().Write(buffer, 0, buffer.Length);
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(buffer, 0, buffer.Length);
                call_status = Post(request, encodingType, out content, out errormsg);
                reqStream.Close();
            }

            return call_status;
        }


        public static bool Post(HttpWebRequest request, Encoding encodingType, out string content, out string errormsg)
        {
            content = string.Empty;
            errormsg = string.Empty;
            bool call_status = false;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encodingType))
                    {
                        call_status = true;
                        content = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }

            return call_status;
        }


        /// <summary>
        /// GET请求数据
        /// </summary>
        /// <returns></returns>
        public static bool Get(string url, out string content, out string errormsg)
        {
            bool success = false;
            content = string.Empty;
            errormsg = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            //request.Timeout = Timeout;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain c, SslPolicyErrors sslPolicyErrors) { return true; };
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                content = myStreamReader.ReadToEnd();
                success = true;
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                success = false;
            }

            return success;
        }


        /// <summary>
        /// GET请求数据
        /// </summary>
        /// <returns></returns>
        public static bool Get(string url, Dictionary<string, string> headers, out string content, out string errormsg)
        {
            bool success = false;
            content = string.Empty;
            errormsg = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            foreach (var h in headers)
                request.Headers.Add(h.Key, h.Value);
            //request.Timeout = Timeout;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain c, SslPolicyErrors sslPolicyErrors) { return true; };
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                content = myStreamReader.ReadToEnd();
                success = true;
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                success = false;
            }

            return success;
        }
    }
}