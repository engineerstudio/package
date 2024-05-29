namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public class Msg
    {
        private static Msg instance = null;
        private static readonly object obj = new object();

        public static Msg Instance
        {
            get
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new Msg();
                    }

                    return instance;
                }
            }
        }


        /// <summary>
        /// 操作失败
        /// </summary>
        public const int Failed = 0;

        public const string FailedMsg = "操作失败";

        /// <summary>
        /// 操作成功
        /// </summary>
        public const int Sucess = 1;

        public const string SucessMsg = "操作成功";


        /// <summary>
        /// 数据库无此对象
        /// </summary>
        public const int ObjectDoesNotExist = 100;

        public const string ObjectDoesNotExistMsg = "对象不存在";


        #region 系统账户  110-119

        public const int DuplicateName = 110;
        public const string DuplicateNameMsg = "账户名重复";

        public const int FalsePassword = 111;
        public const string FalsePasswordMsg = "密码错误";


        public const int NullName = 112;
        public const string NullNameMsg = "名称不能为空";


        public const int OldPasswordError = 113;
        public const string OldPasswordErrorMsg = "旧密码输入不正确";

        #endregion


        #region 系统登录

        /// <summary>
        /// 验证码错误
        /// </summary>
        public const int SignInCaptchaError = 131;

        /// <summary>
        /// 验证码错误
        /// </summary>
        public const string SignInCaptchaErrorMsg = "验证码输入有误";

        /// <summary>
        /// 错误次数超过允许最大失败次数
        /// </summary>
        public const int SignInErrorTimes = 132;

        /// <summary>
        /// 错误次数超过允许最大失败次数
        /// </summary>
        public const string SignInErrorTimesMsg = "错误超过3次，请重新打开浏览器后再进行登录";


        /// <summary>
        /// 错误次数超过允许最大失败次数
        /// </summary>
        public const int SignInPasswordOrUserNameError = 133;

        /// <summary>
        /// 错误次数超过允许最大失败次数
        /// </summary>
        public const string SignInPasswordOrUserNameErrorMsg = "对不起，您输入的用户名或者密码错误";


        /// <summary>
        /// 用户锁定
        /// </summary>
        public const int SignInUserLocked = 134;

        /// <summary>
        /// 用户锁定
        /// </summary>
        public const string SignInUserLockedMsg = "对不起，该用户已经锁定";

        #endregion
    }
}