using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
using Y.Infrastructure.Library.Core.Mapper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.ICache;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;
using static Y.Packet.Entities.Merchants.SectionKey; 

namespace Y.Packet.Services.Merchants
{
    public class SectionDetailService : ISectionDetailService
    {
        private readonly ISectionDetailRepository _repository;
        private readonly ISectionKeyRepository _sectionKeyRepository;
        public SectionDetailService(ISectionDetailRepository repository, ISectionKeyRepository sectionKeyRepository)
        {
            _repository = repository;
            _sectionKeyRepository = sectionKeyRepository;
        }




        public async Task<(bool, IEnumerable<SectionDetail>)> GetBySectionIdAsync(int merchantId, int sectionId)
        {
            if (merchantId == 0 || sectionId == 0) return (false, null);
            string where = $" WHERE MerchantId={merchantId} AND SectionId={sectionId}";
            return (true, await _repository.GetListAsync(where));
        }


        public async Task<(bool, string)> InsertOrUpdateAsync(SectionDetailsModifyModel m)
        {
            //if (m.Id == 0) return (false, "无效标识");
            if (string.IsNullOrEmpty(m.Alias)) return (false, "请输入别名");
            int? rt = 0;

            if (m.Id != 0)
            {
                var model = await _repository.GetAsync(m.Id);
                model.Alias = m.Alias;
                model.Enabled = m.Enabled;
                model.HasSubMenu = m.HasSubMenu;
                model.PageUrl = m.PageUrl;
                model.PcImgUrl = m.PcImgUrl;
                model.H5ImgUrl = m.H5ImgUrl;
                model.SortNo = m.SortNo;
                model.SKey = m.SKey;
                model.Tcontent = m.Tcontent;

                if (string.IsNullOrEmpty(model.SKey)) model.SKey = "";
                if (string.IsNullOrEmpty(model.PageUrl)) model.PageUrl = "";
                if (string.IsNullOrEmpty(model.PcImgUrl)) model.PcImgUrl = "";
                if (string.IsNullOrEmpty(model.Tcontent)) model.Tcontent = "";
                if (model.H5ImgUrl.IsNullOrEmpty()) model.H5ImgUrl = "";

                rt = await _repository.UpdateWithCacheAsync(model);
            }
            else
            {
                var model = new SectionDetail();
                model.SectionId = m.KeyId;
                model.MerchantId = m.MerchantId;
                model.Alias = m.Alias;
                model.Name = m.Alias;
                model.Enabled = m.Enabled;
                model.HasSubMenu = m.HasSubMenu;
                model.PageUrl = m.PageUrl;
                model.PcImgUrl = m.PcImgUrl;
                model.H5ImgUrl = m.H5ImgUrl;
                model.SortNo = m.SortNo;
                model.SKey = m.SKey;
                model.Tcontent = m.Tcontent;

                if (string.IsNullOrEmpty(model.SKey)) model.SKey = "";
                if (string.IsNullOrEmpty(model.PageUrl)) model.PageUrl = "";
                if (string.IsNullOrEmpty(model.PcImgUrl)) model.PcImgUrl = "";
                if (string.IsNullOrEmpty(model.Tcontent)) model.Tcontent = "";
                if (model.H5ImgUrl.IsNullOrEmpty()) model.H5ImgUrl = "";

                rt = await _repository.InsertWithCacheAsync(model);
            }

            if (rt > 0) return (true, "更新成功");
            else return (false, "更新失败");

        }


        public async Task<(bool, string)> UpdateContentAsync(int merchantId, int sectionId, int dId, string detailType, string content)
        {
            if (merchantId == 0) return (false, "商户错误");
            var entity = _repository.Get(dId);
            KeyDetailType t = detailType.ToEnum<KeyDetailType>().Value;
            if (entity == null && t == KeyDetailType.Content && dId == 0)
            {
                var key = _sectionKeyRepository.Get(sectionId);
                var d = new SectionDetail();
                d.SectionId = sectionId;
                d.MerchantId = merchantId;
                d.Name = key.SKey;
                d.Alias = key.Description;
                d.Enabled = true;
                d.HasSubMenu = false;
                d.PcImgUrl = "";
                d.SKey = "";
                d.PageUrl = "";
                d.Tcontent = content;
                var rt = await _repository.InsertWithCacheAsync(d);
                if (rt != null && rt.Value > 0) return (true, "保存成功");
                return (false, "保存失败");
            }
            if (entity.MerchantId != merchantId) return (false, "商户错误");
            if (string.IsNullOrEmpty(content)) content = "";
            entity.Tcontent = content;
            var rt2 = await _repository.UpdateWithCacheAsync(entity);
            if (rt2 > 0) return (true, "更新成功");
            return (false, "更新失败");
        }


        public async Task<(bool, string)> UpdateBannerAsync(int merchantId, int sectionId, List<SectionBanner> banner)
        {
            if (merchantId == 0) return (false, "商户不存在");
            if (banner == null || banner.Count == 0) return (false, "不存在更新");
            var keyMd = await _sectionKeyRepository.GetByMerchantIdAndIdAsync(merchantId, sectionId);
            if (keyMd.DetailType != KeyDetailType.Banner) return (false, "保存类型不正确");

            foreach (var d in banner.Where(t => t.Deleted))
            {
                await _repository.DeleteAsync(d.Id.ToInt32().Value);
            }

            foreach (var d in banner.Where(t => !t.Deleted))
            {
                if (d.Id.ToInt32().Value == 0) // insert
                {
                    var secDetail = new SectionDetail();
                    secDetail.SectionId = sectionId;
                    secDetail.MerchantId = merchantId;
                    secDetail.Name = keyMd.SKey;
                    secDetail.Alias = keyMd.Description;
                    secDetail.Enabled = true;
                    secDetail.HasSubMenu = false;
                    secDetail.PcImgUrl = string.IsNullOrEmpty(d.ImgUrl) ? "" : d.ImgUrl;
                    secDetail.PageUrl = string.IsNullOrEmpty(d.PageUrl) ? "" : d.PageUrl;
                    secDetail.Tcontent = string.IsNullOrEmpty(d.Tcontent) ? "" : d.Tcontent;
                    secDetail.H5ImgUrl = String.IsNullOrEmpty(d.ImgUrl) ? "" : d.ImgUrl;  // todo  this is should be h5 image url
                    secDetail.SKey = "";// string.IsNullOrEmpty(d.SKey) ? "" : d.SKey;
                    await _repository.InsertWithCacheAsync(secDetail);
                }
                else// update
                {
                    var m = await _repository.GetAsync(d.Id.ToInt32().Value);
                    if (m == null) continue;
                    m.PcImgUrl = string.IsNullOrEmpty(d.ImgUrl) ? "" : d.ImgUrl;
                    m.PageUrl = string.IsNullOrEmpty(d.PageUrl) ? "" : d.PageUrl;
                    m.Tcontent = string.IsNullOrEmpty(d.Tcontent) ? "" : d.Tcontent;
                    await _repository.UpdateWithCacheAsync(m);
                }

            }
            return (true, "保存成功");
        }



        /// <summary>
        /// 插入Detial信息
        /// </summary>
        /// <param name="ety"></param>
        /// <returns></returns>
        public async Task<(bool, string)> InsertAsync(List<SectionDetail> ll)
        {
            if (ll == null || ll.Count == 0) return (false, "");
            foreach (var l in ll)
                await _repository.InsertWithCacheAsync(l);

            return (true, "保存完成");
        }

        /// <summary>
        /// 根据MerchantId获取所有的详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SectionDetail>> GetByMerchantIdAsync(int merchantId)
        {
            string where = $" WHERE MerchantId={merchantId} ";

            return await _repository.GetListAsync(where);
        }

    }
}