////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-24 02:23:05                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Y.Packet.Entities.Merchants                                  
*│　类    名：Merchant                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2020-08-24 02:23:05
    /// 
    /// </summary>
    public partial class Merchant
    {

        [NotMapped]
        public Merchant_VipsConfig MerchantVipsConfig
        {
            get
            {
                if (string.IsNullOrEmpty(this.VipsConfig)) return new Merchant_VipsConfig();
                return this.VipsConfig.JsonStringToEntity<Merchant_VipsConfig>();
            }
        }


        [NotMapped]
        public Merchant_SignupConfig MerchantSignupConfig
        {
            get
            {
                if (string.IsNullOrEmpty(this.SignupConfig)) return new Merchant_SignupConfig();
                return this.SignupConfig.JsonStringToEntity<Merchant_SignupConfig>();
            }
        }


        [NotMapped]
        public Merchant_CustomerConfig MerchantCustomerConfig
        {
            get
            {
                if (string.IsNullOrEmpty(this.CustomerConfig)) return new Merchant_CustomerConfig();
                return this.CustomerConfig.JsonStringToEntity<Merchant_CustomerConfig>();
            }
        }


        public enum MerStatus
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 1,
            /// <summary>
            /// 维护
            /// </summary>
            Maintenance = 2,
            /// <summary>
            /// 关闭
            /// </summary>
            Closed = 3
        }


    }
}
