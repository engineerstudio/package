using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.WebInfrastructure.EnumeratedType;
using static Y.Packet.Entities.Members.Users;

namespace Y.Packet.Entities.Members.ViewModels.UserCenter
{
    public class UserBindInfo
    {
        public int MerchantId { get; set; }
        public int MemberId { get; set; }
        public string IdName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 支付密码
        /// </summary>
        public string FPasw { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }

    }
}
