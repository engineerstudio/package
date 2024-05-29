using System;
using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class DefaultString
    {
        /// <summary>
        /// 全局字符串
        /// </summary>
        public static readonly string Sys_Default = "SYS_DEFAULT";

        /// <summary>
        /// 站点默认全局管理员名称
        /// </summary>
        public static readonly string Sys_Admin = "sysadmin";

        #region 默认字符串Config文件里面的UrlConfig的字符串

        public static readonly string UrlConfig_IMG = "IMG";
        public static readonly string UrlConfig_REDIS = "REDIS";

        /// <summary>
        /// 默认下拉字符串Key
        /// </summary>
        public static readonly List<string> SubMenuSkey = new List<string>
        {
            "HEADER-SUB-MENU-SPORT",
            "HEADER-SUB-MENU-ESPORT",
            "HEADER-SUB-MENU-SLOT",
            "HEADER-SUB-MENU-LIVE",
            "HEADER-SUB-MENU-CHESS",
            "HEADER-SUB-MENU-LOTTERY",
            "HEADER-SUB-MENU-HUNT",
            "HEADER-SUB-MENU-FINANCE"
        };

        #endregion


        #region 与Config文件对应的数据库链接字符, 初始化放在DbOption里的

        public static readonly string Sys = "SYS";
        public static readonly string QingDc = "QingDc";
        public static readonly string LogDb = "LOG";
        public static readonly string Esport = "Esport";
        public static readonly string Ying_Promotions = "Ying.Promotions";
        public static readonly string Ying_Merchants = "Ying.Merchants";
        public static readonly string Ying_Sys = "Ying.Sys";
        public static readonly string Ying_Members = "Ying.Users";
        public static readonly string Ying_Games = "Ying.Games";
        public static readonly string ying_Vips = "Ying.Vips";
        public static readonly string Ying_Pay = "Ying.Pay";
        public static readonly string Ying_Redis = "Ying.Redis";


        public static readonly string LuckyGame = "Lucky.Games";
        public static readonly string LuckyRedis = "Lucky.RedisConfig";

        #endregion


        #region 默认密码加密字段

        /// <summary>
        /// [登陆密码]Aes加密关键字
        /// </summary>
        public static readonly string AesEncryptKeys = "AesAaron";

        /// <summary>
        /// [资金密码] Aes加密关键字
        /// </summary>
        public static readonly string AesEncryptKeysForFunds = "AesFunds";

        /// <summary>
        /// 系统默认密码
        /// </summary>
        public const string DefaultPassword = "psw123456";

        #endregion


        #region 数据库默认字段

        /// <summary>
        /// 系统默认时间
        /// </summary>
        public static readonly DateTime DefaultDateTime = Convert.ToDateTime("1900-01-01");

        #endregion


        /// <summary>
        /// 默认图片链接地址
        /// </summary>
        public static readonly string DefaultImage = "/upload/d/space.png";
    }
}