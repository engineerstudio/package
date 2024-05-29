using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    public class WebApiResultErrorCode
    {
        [Description("站点不存在")] public static readonly string Site_Error_Not_Exit = "SITE_ERROR_NOT_EXIT";

        [Description("用户名已经存在")] public static readonly string Error_Name_Exit_Name = "EXIT_USER_NAME";
        [Description("用户不存在")] public static readonly string Error_Name_NOT_Exit = "EXIT_USER_NOT_EXIT";
        [Description("用户名不得为空")] public static readonly string Error_Name_Null = "INCORRECT_FORMAT_NULL";
        [Description("用户名格式错误")] public static readonly string Error_Name = "INCORRECT_FORMAT";


        [Description("保存失败")] public static readonly string Error = "ERROR";
        [Description("保存成功")] public static readonly string Sucess = "SUCESS";

        /// <summary>
        /// 参数为空错误
        /// </summary>
        [Description("参数错误")] public static readonly string Error_Parameter = "PARAMETER_ERROR";


        [Description("用户名不得为空")] public static readonly string Error_Member_Null = "ERROR_MEMBER_NAME_NULL";
        [Description("转账单号不得为空")] public static readonly string Error_Trans_Ref_Null = "ERROR_TRANS_REF_NULL";

        [Description("转账金额不低于10")] public static readonly string Error_Amount_Limit = "AMOUNT_ERROR_LIMIT";
        [Description("取款金额应小于0")] public static readonly string Error_Amount_Draw = "AMOUNT_ERROR_DRAW";

        /// <summary>
        /// 程序运行错误
        /// </summary>
        [Description("程序运行错误")] public static readonly string Error_Program = "PROGRAM_ERROR";


        /// <summary>
        /// 密钥错误
        /// </summary>
        [Description("请求密钥错误")] public static readonly string Error_Authorization = "AUTHORIZATION_ERROR";

        /// <summary>
        /// 未授权的请求访问
        /// </summary>
        [Description("未授权的请求访问")] public static readonly string Error_IP = "AUTHORIZATION_IP_ERROR";
    }
}