using System;
using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.YEntity
{
    public class PayDbAttribute : Attribute
    {
        public string Name { get; set; } // 参数含义
        public PaySQLDbType DbType { get; set; }
        public PayConfig IsConfig { get; set; } // 如果需要配置，那么就是会显示在前端进行配置的字符串，取Name字段为名字
        public object DefaultStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">名称/说明</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="isConfig">是否需要前台配置</param>
        /// <param name="defaultStr">默认字符串</param>
        public PayDbAttribute(string name, PaySQLDbType dbType, PayConfig isConfig, object defaultStr = null)
        {
            Name = name;
            DbType = dbType;
            IsConfig = isConfig;
            DefaultStr = defaultStr;
        }
    }


    #region 隐藏无用

    ///// <summary>
    ///// 数据格式
    ///// </summary>
    //public class PaySQLDbType
    //{
    //    /// <summary>
    //    /// 网关
    //    /// </summary>
    //    public string GateWay { get; set; }
    //    /// <summary>
    //    /// 密钥
    //    /// </summary>
    //    public string Key { get; set; }
    //    /// <summary>
    //    /// 私钥
    //    /// </summary>
    //    public string PrivateKey { get; set; }
    //    /// <summary>
    //    /// 公钥
    //    /// </summary>
    //    public string PublicKey { get; set; }
    //    /// <summary>
    //    /// 订单字段标记
    //    /// </summary>
    //    public string OrderId { get; set; }
    //    /// <summary>
    //    /// 金额  整数
    //    /// </summary>
    //    public string MoneyInteger { get; set; }
    //    /// <summary>
    //    /// 金额  小数点2位数
    //    /// </summary>
    //    public string Money2Decimal { get; set; }
    //    /// <summary>
    //    /// 回调地址标记
    //    /// </summary>
    //    public string Url { get; set; }
    //    /// <summary>
    //    /// IP字段 
    //    /// </summary>
    //    public string IP { get; set; }
    //    /// <summary>
    //    /// 随机数
    //    /// </summary>
    //    public string Random { get; set; }
    //    /// <summary>
    //    /// 日期字段 年月日时分秒
    //    /// </summary>
    //    public string DateTime { get; set; }
    //    /// <summary>
    //    /// 日期字段 秒时间戳
    //    /// </summary>
    //    public string Second { get; set; }
    //    /// <summary>
    //    /// 日期字段 毫秒时间戳
    //    /// </summary>
    //    public string Millisecond { get; set; }
    //    /// <summary>
    //    /// 银行名称
    //    /// </summary>
    //    public string BankName { get; set; }
    //    /// <summary>
    //    /// 银行账号
    //    /// </summary>
    //    public string BankAccount { get; set; }
    //    /// <summary>
    //    /// 银行账户名称
    //    /// </summary>
    //    public string AccountName { get; set; }
    //}

    #endregion

    public enum PaySQLDbType
    {
        [Description("网关")] GateWay,
        [Description("密钥")] Key,
        [Description("私钥")] PrivateKey,
        [Description("公钥")] PublicKey,
        [Description("订单号")] String,
        [Description("订单号")] OrderId,
        [Description("金额整数")] MoneyInteger,
        [Description("金额小数点2位数")] Money2Decimal,
        [Description("回调地址标记")] Url,
        [Description("IP字段")] IP,
        [Description("随机数")] Random,
        [Description("日期字段 年月日时分秒")] DateTime,
        [Description("日期字段 秒时间戳")] Second,
        [Description("日期字段 毫秒时间戳")] Millisecond,
        [Description("银行名称")] BankName,
        [Description("银行账号")] BankAccount,
        [Description("用户银行账户名称")] BankAccountName
    }

    /// <summary>
    /// 是否前台配置
    /// </summary>
    public enum PayConfig
    {
        [Description("是")] True,
        [Description("否")] False
    }


    public class PaySubmitResult
    {
        /// <summary>
        /// 是否请求第三方接口成功
        /// </summary>
        public bool RequstSucess { get; set; }

        public string Msg { get; set; }
        public PayOrderStatus Status { get; set; }
        public int OrderId { get; set; }
        public string ResponseStr { get; set; }
    }

    /// <summary>
    /// 代付回调类
    /// </summary>
    public class WithdrawalsSubmitResult : PaySubmitResult
    {
        public string WithdrawalsMerchantOrderId { get; set; }
    }

    public class DepositedSubmitResult : PaySubmitResult
    {
        public string Url { get; set; }
        /// <summary>
        /// 第三方系统的订单ID
        /// </summary>
        public string SysId { get; set; }
    }


    /// <summary>
    /// 支付/代付  状态
    /// </summary>
    public enum PayOrderStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")] None,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")] Audited,

        /// <summary>
        /// 处理中
        /// </summary>
        [Description("处理中")] Processing,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")] Sucess,

        /// <summary>
        /// 失败 
        /// </summary>
        [Description("失败")] Failed
    }

}