using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Cache.Redis.MerchantsService;
using Y.Infrastructure.ICache;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;

namespace Y.Packet.Services.Merchants
{
    public class SectionKeyService : SectionKeyCacheService, ISectionKeyService
    {
        private readonly ISectionKeyRepository _repository;
        private readonly ISectionDetailRepository _detailRepository;

        public SectionKeyService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, ISectionKeyRepository repository, ISectionDetailRepository detailRepository) : base(options, factory)
        {
            _repository = repository;
            _detailRepository = detailRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SectionDetail>> GetByKeyAsync(int merchantId, string key)
        {
            // TODO 商户是否包含这个Key
            if (string.IsNullOrEmpty(key)) return null;
            if (merchantId == 0) return null;

            var cache = await base.GetByKeyCacheAsync(merchantId, key);
            if (cache.Exist) return JsonHelper.JSONToObject<IEnumerable<SectionDetail>>(cache.Data);

            SectionKey skey = await _repository.GetByKeyAsync(merchantId, key);
            if (skey == null) return null;

            //var valueList = await _detailRepository.GetListAsync($"WHERE SectionId={skey.Id} AND MerchantId={merchantId}", null);
            var valueList = await _detailRepository.GetListAsync(merchantId, null, skey.Id);

            var cacheValue = JsonHelper.ToJson(valueList);
            await base.SaveGetByKeyCacheAsync(merchantId, key, cacheValue);

            return valueList;
        }

        /// <summary>
        /// 根据Id获取到key string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(int id)
        {
            if (id == 0) return string.Empty;
            var rt = await _repository.GetAsync(id);
            return rt.SKey;
        }

        /// <summary>
        /// 针对 H5 的输出  Banner game 通知公告
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="secIds"></param>
        /// <returns></returns>
        public async Task<List<SectionH5VM>> GetByMerchantIdForH5Async(int merchantId, string[] secIds)
        {
            string md5 = MD5EncryptHelper.ToMD5(secIds.ArrayToString());

            var cache = await base.GetByKeyCacheAsync(merchantId, md5);
            if (cache.Exist) return JsonHelper.JSONToObject<List<SectionH5VM>>(cache.Data);


            // 1. 根据skey获取到对应的 SectionKey的Id
            //var sections = await _repository.GetListAsync($" WHERE MerchantId={merchantId} AND SKey IN ({secIds.ToCommaSplitString2()})");
            var sections = await _repository.GetListAsync(merchantId, secIds);

            // 2. 根据1的Id获取到详细的内容
            //var secDetails = await _detailRepository.GetListAsync($" WHERE MerchantId={merchantId} AND Enabled=1");
            var secDetails = await _detailRepository.GetListAsync(merchantId, true, null);
            var list = new List<SectionH5VM>();
            // 3. 整理输出
            foreach (SectionKey sec in sections)
            {
                var secVM = new SectionH5VM();
                secVM.Key = sec.SKey;
                secVM.Details = secDetails.Where(t => t.SectionId == sec.Id).Select(t => new SectionDForH5VM()
                {
                    Title = t.Alias,
                    Img = t.H5ImgUrl,
                    Page = t.PageUrl
                }).ToList();

                // TODO 判断该游戏站点是否已经开启

                if (secVM.Details.Count != 0)
                    list.Add(secVM);
            }

            var cacheValue = JsonHelper.ToJson(list);
            await base.SaveGetByMerchantIdForH5CacheAsyncc(merchantId, md5, cacheValue);

            return list;
        }

        /// <summary>
        /// PC的菜单
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<List<SectionDVM>> GetByMerchantIdForPcTopMenusAsync(int merchantId)
        {
            var sectionKey = await _repository.GetByKeyAsync(merchantId, "HEADER-MENU");
            //var details = await _detailRepository.GetListAsync($" WHERE MerchantId={merchantId} AND Enabled=1 AND SectionId={sectionKey.Id}");
            var details = await _detailRepository.GetListAsync(merchantId, true, sectionKey.Id);
            List<SectionDVM> result = new List<SectionDVM>();
            foreach (var t in details)
            {
                var v = new SectionDVM()
                {
                    Title = t.Alias,
                    Img = t.PcImgUrl,
                    PUrl = t.PageUrl,
                    HasSub = t.HasSubMenu,
                    SubKey = t.SKey,
                    Txt = t.Tcontent,
                    SubD = null,
                    SortNo = t.SortNo
                };
                result.Add(v);
            }
            return result;
        }

        /// <summary>
        /// PC菜单 部分菜单顶部有图片的那种
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<List<SectionDVMV2>> GetByMerchantIdForPcTopMenusV2Async(int merchantId)
        {
            var sectionKey = await _repository.GetByKeyAsync(merchantId, "HEADER-MENU");
            //var details = await _detailRepository.GetListAsync($" WHERE MerchantId={merchantId} AND Enabled=1 AND SectionId={sectionKey.Id}");
            var details = await _detailRepository.GetListAsync(merchantId, true, sectionKey.Id);
            List<SectionDVMV2> result = new List<SectionDVMV2>();
            foreach (var t in details)
            {
                var v = new SectionDVMV2()
                {
                    Title = t.Alias,
                    Img = t.PcImgUrl,
                    PUrl = t.PageUrl,
                    HasSub = t.HasSubMenu,
                    SubKey = t.SKey,
                    Txt = t.Tcontent,
                    SubD = null,
                    SortNo = t.SortNo,
                    ShowTop = t.IsTopImg
                };
                result.Add(v);
            }
            return result;
        }


        /// <summary>
        /// 子菜单，针对Pc端的输出
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<List<SectionVM>> GetAllByMerchantIdForPcAsync(int merchantId)
        {
            // SectionVM
            var sections = await _repository.GetListAsync($" WHERE MerchantId={merchantId} ");
            //var secDetails = await _detailRepository.GetListAsync($" WHERE MerchantId={merchantId} AND Enabled=1");
            var secDetails = await _detailRepository.GetListAsync(merchantId, true, null);
            var list = new List<SectionVM>();
            foreach (SectionKey sec in sections)
            {
                var secVM = new SectionVM();
                secVM.Key = sec.SKey;
                secVM.Details = secDetails.Where(t => t.SectionId == sec.Id).Select(t => new SectionDVM()
                {
                    Title = t.Alias,
                    Img = t.PcImgUrl,
                    PUrl = t.PageUrl,
                    HasSub = t.HasSubMenu,
                    SubKey = t.SKey,
                    Txt = t.Tcontent,
                    SubD = null
                }).ToList();


                switch (sec.SKey)
                {
                    case "GAME-CATEGORY": // 特殊处理，游戏包含具体的游戏内容
                        foreach (var vm in secVM.Details)
                        {
                            vm.SubD = secDetails.Where(t => t.Enabled && t.PageUrl.StartsWith($"{vm.PUrl}_")).Select(t => new SectionDVM()
                            {
                                Title = t.Alias,
                                Img = t.PcImgUrl,
                                PUrl = t.PageUrl,
                                HasSub = t.HasSubMenu,
                                SubKey = t.SKey,
                                Txt = t.Tcontent,
                                SubD = null
                            }).ToList();
                        }
                        break;
                    default:
                        break;
                }


                list.Add(secVM);
            }
            return list;
        }

        public async Task<(bool, IEnumerable<SectionKey>)> GetByMerchantIdAsync(int merchantId)
        {
            string where = $" WHERE MerchantId={merchantId} ";
            return (true, await _repository.GetListAsync(where));
        }

        /// <summary>
        /// 批量添加section key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<(bool, string, int)> InsertAsync(SectionKey key)
        {
            int? rt = await _repository.InsertWithCacheAsync(key);

            if (rt == null || rt < 1) return (false, "保存失败", 0);

            return (true, "", rt.Value);
        }

    }
}