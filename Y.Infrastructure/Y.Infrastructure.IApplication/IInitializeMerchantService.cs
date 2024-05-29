﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.IApplication
{
    public interface IInitializeMerchantService
    {

        /// <summary>
        /// 创建默认站点
        /// 1. 默认站点创建
        /// 2. 默认模板写入
        /// </summary>
        void CreateMerchant();

        /// <summary>
        /// 创建站点的默认数据
        /// 1. 默认代理
        /// 2. 默认密码
        /// 3. 默认banner写入
        /// 4. 默认底部规则标题写入
        /// 3. 默认活动规则.  代理返点,会员返水,充值优惠
        /// </summary>
        void CreateDefaultD();


    }
}
