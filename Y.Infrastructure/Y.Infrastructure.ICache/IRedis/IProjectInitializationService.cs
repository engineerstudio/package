using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;

namespace Y.Infrastructure.ICache
{
    public interface IProjectInitializationService : IBaseRedisRepository
    {

        /// <summary>
        /// 获取System总后台的页面初始缓存
        /// </summary>
        /// <returns></returns>
        string GetSysProjectPageData();
        /// <summary>
        /// 设定System总后台的页面初始缓存
        /// </summary>
        /// <param name="data"></param>
        void SetSysProjectPageData(string data);



        /// <summary>
        /// 获取Merchant总后台的页面初始缓存
        /// </summary>
        /// <returns></returns>
        string GetMerchantProjectPageData();
        /// <summary>
        /// 设定Merchant总后台的页面初始缓存
        /// </summary>
        /// <param name="data"></param>
        void SetMerchantProjectPageData(string data);

    }



}
