using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants
{
    public class Merchant_CustomerConfig
    {
        /// <summary>
        /// PC站点的Logo
        /// </summary>
        public string PcLogo { get; set; }
        /// <summary>
        /// H5的站点的Logo
        /// </summary>
        public string H5Logo { get; set; }
        /// <summary>
        /// 在线客服链接
        /// </summary>
        public string ServiceLink { get; set; }
        /// <summary>
        /// 下载APP的二维码
        /// </summary>
        public string DownloadQRCode { get; set; }
        /// <summary>
        /// H5站点UrL
        /// </summary>
        public string H5SiteUrl { get; set; }

        /// <summary>
        /// 是否允许H5访问，如果不允许，那么直接进入APP引导下载页面
        /// </summary>
        public bool EnabledH5Visit { get; set; }


        /// <summary>
        /// 开启代理模式，普通用户无法注册，只能通过代理的二维码进行注册
        /// </summary>
        public bool EnabledAgentPattern { get; set; }
    }
}
