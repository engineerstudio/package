using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CaptchaCode { get; set; }
        public string Ip { get; set; }
        public string ReturnUrl { get; set; }
    }
}
