using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;

namespace Y.Packet.Services.Merchants
{
    public class HelpAreaTypeService : IHelpAreaTypeService
    {
        private readonly IHelpAreaTypeRepository _repository;
        private readonly IHelpAreaRepository _helpAreaRepository;
        public HelpAreaTypeService(IHelpAreaTypeRepository repository, IHelpAreaRepository helpAreaRepository)
        {
            _repository = repository;
            _helpAreaRepository = helpAreaRepository;
        }

        public async Task<Dictionary<int, string>> GetDicAsync(int merchantId)
        {
            var rt = await _repository.GetListAsync($" WHERE MerchantId = {merchantId} ");
            return rt.ToDictionary(t => t.Id, t => t.Title);
        }


        public async Task<(bool, string)> InsertOrModifyAsync(HelpAreaTypeAOM aom)
        {
            if (aom == null) return (false, "对象错误");
            if (string.IsNullOrEmpty(aom.Title)) return (false, "请输入标题名称");
            int? rt = null;
            if (aom.Id == 0)
            {
                var m = new HelpAreaType();
                m.IsHref = aom.IsHref;
                m.Href = string.IsNullOrEmpty(aom.Href) ? "" : aom.Href;
                m.Title = aom.Title;
                m.IsOpen = aom.IsOpen;
                m.CreateTime = DateTime.UtcNow.AddHours(8);
                m.MerchantId = aom.MerchantId;
                m.IconImg = aom.IconImg;

                rt = await _repository.InsertAsync(m);
            }
            else
            {
                if (aom.Id == 0) return (false, "更新ID错误");
                var m = await _repository.GetAsync(aom.Id);
                m.Href = string.IsNullOrEmpty(aom.Href) ? "" : aom.Href;
                m.IsHref = aom.IsHref;
                m.IsOpen = aom.IsOpen;
                m.Title = aom.Title;
                m.IconImg = aom.IconImg;
                rt = await _repository.UpdateAsync(m);
            }

            if (rt != null && rt.Value > 0) return (true, "保存成功");

            return (false, "保存失败");

        }



        public async Task<(IEnumerable<HelpAreaType>, int)> GetPageListAsync(int merchantId)
        {
            string conditions = $"WHERE MerchantId={merchantId}";
            var rt = await _repository.GetListAsync(conditions);
            var count = await _repository.RecordCountAsync(conditions);
            return (rt, count);
        }


        public async Task<(bool, string)> DeleteAsync(int merchantId, int id)
        {
            if (merchantId == 0 || id == 0) return (false, "标识错误");
            var entiry = await _repository.GetAsync(id);
            if (entiry == null) return (false, "未查询到数据");
            if (entiry.MerchantId != merchantId) return (false, "未查询到数据");
            var rt = await _repository.DeleteAsync(id);
            return rt.ToResult("删除成功", "删除失败");
        }

        /// <summary>
        /// 获取到帮助列表及下级
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HelpAreaTypeVM2>> GetHelpAreaTypesAsync(int merchantId)
        {
            string conditions = $"WHERE MerchantId={merchantId}";
            var typeList = await _repository.GetListAsync(conditions);
            //var typeDataList = typeList.Select(t => new HelpAreaTypeVM2()
            //{
            //    Title = t.Title,
            //    SortDesc = t.SortNo
            //});
            List<HelpAreaTypeVM2> typeDataList = new List<HelpAreaTypeVM2>();
            var contentList = await _helpAreaRepository.GetListAsync(conditions);
            foreach (var t in typeList)
            {
                typeDataList.Add(new HelpAreaTypeVM2()
                {
                    Title = t.Title,
                    SortDesc = t.SortNo,
                    Sub = contentList.Where(c => c.TypeId == t.Id).Select(s => new HelpAreaVMV2()
                    {
                        Title = s.Title,
                        Link = $@"/help/{s.TypeId}/{s.Id}",
                        SortDesc = s.SortNo
                    }).ToList()
                });
            }

            return typeDataList;
        }

        /// <summary>
        /// 获取到带着图片的帮助列表及下级
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HelpAreaTypeVM3>> GetHelpAreaTypesWithCategoryIconImagesAsync(int merchantId)
        {
            string conditions = $"WHERE MerchantId={merchantId}";
            var typeList = await _repository.GetListAsync(conditions);
            //var typeDataList = typeList.Select(t => new HelpAreaTypeVM2()
            //{
            //    Title = t.Title,
            //    SortDesc = t.SortNo
            //});
            List<HelpAreaTypeVM3> typeDataList = new List<HelpAreaTypeVM3>();
            var contentList = await _helpAreaRepository.GetListAsync(conditions);
            foreach (var t in typeList)
            {
                typeDataList.Add(new HelpAreaTypeVM3()
                {
                    Title = t.Title,
                    SortDesc = t.SortNo,
                    Icon = t.IconImg,
                    Sub = contentList.Where(c => c.TypeId == t.Id).Select(s => new HelpAreaVMV2()
                    {
                        Title = s.Title,
                        Link = $@"/help/{s.TypeId}/{s.Id}",
                        SortDesc = s.SortNo
                    }).ToList()
                });
            }

            return typeDataList;
        }


    }
}