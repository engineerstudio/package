using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class UploadController : ControllerBase
    {


        private readonly IWebHostEnvironment webHostingEnv;
        private readonly IConfiguration _configuration;
        IHttpContextAccessor contextAccessor;
        //private readonly Dictionary<string, string> _sysconfig;
        string img_url = string.Empty;
        public UploadController(IWebHostEnvironment webHostingEnv, IHttpContextAccessor contextAccessor, IOptionsSnapshot<Dictionary<string, string>> options)
        {
            this.webHostingEnv = webHostingEnv;
            this.contextAccessor = contextAccessor;
            img_url = options.Get(DefaultString.Sys_Default)[DefaultString.UrlConfig_IMG];
        }




        /// <summary>
        /// 图片上传功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("image")]
        public string UploadImage()
        {
            #region 文件上传
            var imgFile = Request.Form.Files[0];
            if (imgFile != null && !imgFile.FileName.IsNullOrEmpty())
            {
                long size = 0;
                string tempname = "";
                var filename = ContentDispositionHeaderValue
                                .Parse(imgFile.ContentDisposition)
                                .FileName
                                .Trim('"');
                var extname = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));

                #region 判断后缀
                //if (!extname.ToLower().Contains("jpg") && !extname.ToLower().Contains("png") && !extname.ToLower().Contains("gif"))
                //{
                //    return Json(new { code = 1, msg = "只允许上传jpg,png,gif格式的图片.", });
                //}
                #endregion

                #region 判断大小
                long mb = imgFile.Length / 1024 / 1024; // MB
                if (mb > 1)
                {
                    return (new { code = 1, msg = "只允许上传小于 1MB 的图片.", }).ToJson();
                }
                #endregion

                var filename1 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000, 9999) + extname;
                tempname = filename1;
                var path = webHostingEnv.WebRootPath;
                string dir = DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(webHostingEnv.WebRootPath + $@"\upload\{dir}"))
                {
                    Directory.CreateDirectory(webHostingEnv.WebRootPath + $@"\upload\{dir}");
                }
                filename = webHostingEnv.WebRootPath + $@"\upload\{dir}\{filename1}";
                size += imgFile.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    imgFile.CopyTo(fs);
                    fs.Flush();
                }

                string result = UpdateLoadToServer(filename, Request.ContentType);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);

                return (new { code = jo["Code"].ToString().ToInt32(), msg = jo["Msg"].ToString().ToInt32(), info = new { src = $"{jo["Path"]}", title = "图片标题" } }).ToJson();
            }
            return (new { code = 1, msg = "上传失败", }).ToJson();
            #endregion
        }

        /// <summary>
        /// Kindeditor 图片上传
        /// </summary>
        /// <returns>Kindeditor的专用返回链接</returns>
        [HttpPost("kindeditor")]
        public string KindeditorUploadImage()
        {
            // 参考链接 https://www.cnblogs.com/mozq/p/11143988.html
            string upResult = ImageUpload();
            // error 失败为0, 成功为1
            if (upResult.IsNullOrEmpty()) return (new { error = 1, message = "上传失败,未获取到文件" }).ToJson();
            JObject jo = (JObject)JsonConvert.DeserializeObject(upResult);

            return (new { error = 0, url = $"{img_url}{jo["Path"]}" }).ToJson();
        }

        [HttpGet("ImageUpload2222")]
        private string ImageUpload()
        {
            string result = string.Empty;

            var imgFile = Request.Form.Files[0];
            if (imgFile != null && !imgFile.FileName.IsNullOrEmpty())
            {
                long size = 0;
                string tempname = "";
                var filename = ContentDispositionHeaderValue
                                .Parse(imgFile.ContentDisposition)
                                .FileName
                                .Trim('"');
                var extname = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf("."));

                #region 判断后缀
                //if (!extname.ToLower().Contains("jpg") && !extname.ToLower().Contains("png") && !extname.ToLower().Contains("gif"))
                //{
                //    return Json(new { code = 1, msg = "只允许上传jpg,png,gif格式的图片.", });
                //}
                #endregion

                #region 判断大小
                long mb = imgFile.Length / 1024 / 1024; // MB
                if (mb > 1)
                {
                    return (new { code = 1, msg = "只允许上传小于 1MB 的图片.", }).ToJson();
                }
                #endregion

                var filename1 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000, 9999) + extname;
                tempname = filename1;
                var path = webHostingEnv.WebRootPath;
                string dir = DateTime.Now.ToString("yyyyMMdd");

                // TODO 此处需要判断是否是Linux运行环境 
                if (!Directory.Exists(webHostingEnv.WebRootPath + $@"\upload\{dir}"))
                {
                    Directory.CreateDirectory(webHostingEnv.WebRootPath + $@"\upload\{dir}");
                }
                filename = webHostingEnv.WebRootPath + $@"\upload\{dir}\{filename1}";


                size += imgFile.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    imgFile.CopyTo(fs);
                    fs.Flush();
                }

                result = UpdateLoadToServer(filename, Request.ContentType);
            }

            return result;
        }


        [HttpGet("ImageUpload223322")]
        private string UpdateLoadToServer(string filePath, string contentType)
        {
            //FileStream fs = System.IO.File.ReadAllBytes(filePath);
            //byte[] data = new byte[fs.Length];
            byte[] data = System.IO.File.ReadAllBytes(filePath);
            //fs.Read(data, 0, data.Length);
            //fs.Close();  

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{img_url}/ImageBytes");
            request.ContentType = contentType;
            request.Method = "POST";
            Encoding encoding = Encoding.UTF8;
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Flush();
            requestStream.Close();


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding);
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();

            return retString;
        }



    }
}
