using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Cache.Redis;
using Y.Infrastructure.Cache.Redis.MerchantsService;
using Y.Infrastructure.ICache.IRedis.MerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Entities.Merchants.ViewModels.Sys;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;
using static Y.Packet.Entities.Merchants.Merchant;

namespace Y.Packet.Services.Merchants
{
    public class MerchantService : MerchantCacheService, IMerchantService
    {
        private readonly IMerchantRepository _repository;

        public MerchantService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IMerchantRepository repository) : base(options, factory)
        {
            _repository = repository;
        }

        /// <summary>
        /// 添加站点
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string, int)> AddAsync(string merchName, string pcTemplet, string h5Templet)
        {
            var merch = new Merchant()
            {
                Name = merchName,
                GameCredit = 0,
                Status = MerStatus.Normal,
                CreateDate = DateTime.UtcNow.AddHours(8),
                PageSectionConfig = "",
                Domains = "",
                IpWhitelist = "",
                VipsConfig = "",
                PcTempletStr = pcTemplet,
                H5TempletStr = h5Templet,
                SignupConfig = new Merchant_SignupConfig().ToJson(),
                CustomerConfig = new Merchant_CustomerConfig().ToJson()
            };
            // TODO 判断站点名字是否存在，存在则返回

            var result = await _repository.InsertWithCacheAsync(merch);
            if (result == null || result.Value < 1) return (false, "保存站点失败 !", 0);
            return (true, "保存站点成功 !", result.Value);
        }

        public async Task<(IEnumerable<Merchant>, int)> GetPageListAsync(MerchantListQuery q)
        {
            string conditions = "WHERE 1=1 ";

            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), await _repository.RecordCountAsync());
        }

        /// <summary>
        /// 获取所有的商户
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Merchant>> GetMerchantsAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<Dictionary<int, string>> MerchantsDicAsync() => await _repository.GetMerchantIdAndNameDicAsync();


        /// <summary>
        /// 根据域名找出来
        /// </summary>
        /// <param name="domains"></param>
        /// <returns></returns>
        public async Task<(bool, int)> IsValidDoaminNameAsync(string domains)
        {
            var dic = await _repository.GetAllDomainsAsync();
            bool exist = dic.ContainsKey(domains);
            if (!exist) return (exist, 0);
            return (exist, dic[domains]);
        }

        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, Merchant)> GetAsync(int merchantId)
        {
            if (merchantId == 0) return (false, null);
            var entity = await _repository.GetFromCacheAsync(merchantId);
            if (entity == null) return (false, null);
            return (true, entity);
        }

        public async Task<(bool, string)> UpdateCreditAsync(int merchantId, decimal credit)
        {
            if (merchantId == 0 || credit == 0) return (false, "参数错误");
            var merchantResult = await this.GetAsync(merchantId);
            if (!merchantResult.Item1) return (false, "未查询到商户");
            var rt = await _repository.UpdateCrediWithCacheAsync(merchantId, credit);
            return rt.ToResult("保存成功", "保存失败");
        }

        public async Task<(bool, string)> UpdatePcLogoAsync(int merchantId, string link)
        {
            if (merchantId == 0) return (false, null);
            var entity = await _repository.GetAsync(merchantId);
            entity.MerchantCustomerConfig.PcLogo = link;
            entity.CustomerConfig = entity.MerchantCustomerConfig.ToJson();

            var rt = await _repository.UpdateWithCacheAsync(entity);
            return rt.ToResult("保存成功", "保存失败");
        }

        public async Task<(bool, string)> UpdateH5LogoAsync(int merchantId, string link)
        {
            if (merchantId == 0) return (false, null);
            var entity = await _repository.GetAsync(merchantId);
            entity.MerchantCustomerConfig.H5Logo = link;
            entity.CustomerConfig = entity.MerchantCustomerConfig.ToJson();
            var rt = await _repository.UpdateWithCacheAsync(entity);
            return rt.ToResult("保存成功", "保存失败");
        }

        public async Task<(bool, string)> UpdateServiceLinkAsync(int merchantId, string link)
        {
            if (merchantId == 0) return (false, null);
            var entity = await _repository.GetAsync(merchantId);
            entity.MerchantCustomerConfig.ServiceLink = link;
            entity.CustomerConfig = entity.MerchantCustomerConfig.ToJson();
            var rt = await _repository.UpdateWithCacheAsync(entity);
            return rt.ToResult("保存成功", "保存失败");
        }

        public async Task<(bool, string)> UpdateCustomerConfigAsync(string name, int merchantId, string pclogo, string h5logo, string servicelink, string downloadQr, string siteUrl)
        {
            if (merchantId == 0) return (false, null);
            if (name.IsNullOrEmpty()) return (false, "请输入站点名称");
            var entity = await _repository.GetAsync(merchantId);
            entity.Name = name;
            Merchant_CustomerConfig config = new Merchant_CustomerConfig();
            config.PcLogo = pclogo;
            config.H5Logo = h5logo;
            config.ServiceLink = servicelink;
            config.DownloadQRCode = downloadQr;
            config.H5SiteUrl = siteUrl;
            entity.CustomerConfig = config.ToJson();
            var rt = await _repository.UpdateWithCacheAsync(entity);
            return rt.ToResult("保存成功", "保存失败");
        }


        public async Task<(bool, string, Merchant)> Get2Async(int merchantId)
        {
            if (merchantId == 0) return (false, "不存在的商户Id， 滚 ， 别来烦我", null);
            var entiry = await _repository.GetFromCacheAsync(merchantId);
            if (entiry == null) return (false, "不存在的商户", null);
            return (true, string.Empty, entiry);
        }

        #region VIP全局设置

        public async Task<(bool, dynamic)> GetVipsConfigAsync(int merchantId)
        {
            var rt = await Get2Async(merchantId);
            if (!rt.Item1) return (rt.Item1, rt.Item2);

            return (rt.Item1, rt.Item3.VipsConfig);
        }

        public async Task<(bool, string)> SaveVipsConfigAsync(int merchantId, Merchant_VipsConfig config)
        {
            var rt = await Get2Async(merchantId);
            if (!rt.Item1) return (rt.Item1, rt.Item2);
            rt.Item3.VipsConfig = config.ToJson();
            var r = await _repository.UpdateWithCacheAsync(rt.Item3);
            return r.ToResult("保存成功", "保存失败");
        }

        #endregion


        #region 站点Logo等基础配置

        public async Task<(bool, string)> GetCustomerConfigAsync(int merchantId)
        {
            var rt = await Get2Async(merchantId);
            if (!rt.Item1) return (rt.Item1, rt.Item2);
            return (rt.Item1, rt.Item3.CustomerConfig);
        }

        /// <summary>
        /// 保存站点基本配置
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> SaveCustomerConfigAsync(int merchantId, Merchant_CustomerConfig csConfig)
        {
            var entity = (await Get2Async(merchantId)).Item3;
            entity.CustomerConfig = csConfig.ToJson();

            var signUpConfig = entity.MerchantSignupConfig;
            signUpConfig.EnableCode = csConfig.EnabledAgentPattern;
            entity.SignupConfig = signUpConfig.ToJson();

            var rt = await _repository.UpdateWithCacheAsync(entity);
            return rt.ToResult("保存成功", "保存失败");
        }

        #endregion


    }
}