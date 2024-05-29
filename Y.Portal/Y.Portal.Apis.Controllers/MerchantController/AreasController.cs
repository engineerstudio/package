using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.WebInfrastructure.Entity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Merchants;
using Y.Packet.Services.IMerchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Portal.Apis.Controllers.Helper;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IGames;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IBaseHandlerService _baseHandlerService;

        private readonly ISectionKeyService _sectionKeyService;
        private readonly ISectionDetailService _sectionDetailService;

        private readonly IHelpAreaTypeService _helpAreaTypeService;
        private readonly IHelpAreaService _helpAreaService;

        private readonly INoticeAreaService _noticeAreaService;
        private readonly INoticeInfoService _noticeInfoService;

        private readonly IUsersService _usersService;

        private readonly IGameInfoService _gameInfoService;
        private readonly IGameMerchantService _gameMerchantService;



        public AreasController(IBaseHandlerService baseHandlerService, ISectionKeyService sectionKeyService,
            ISectionDetailService sectionDetailService,
            IHelpAreaTypeService helpAreaTypeService,
            IHelpAreaService helpAreaService,
            INoticeAreaService noticeAreaService,
            INoticeInfoService noticeInfoService,
            IUsersService usersService,
            IGameInfoService gameInfoService,
            IGameMerchantService gameMerchantService)
        {
            _baseHandlerService = baseHandlerService;
            _sectionKeyService = sectionKeyService;
            _sectionDetailService = sectionDetailService;
            _helpAreaTypeService = helpAreaTypeService;
            _helpAreaService = helpAreaService;
            _noticeAreaService = noticeAreaService;
            _noticeInfoService = noticeInfoService;
            _usersService = usersService;
            _gameInfoService = gameInfoService;
            _gameMerchantService = gameMerchantService;
        }


        #region 区域管理 SECTION


        [HttpPost("section")]
        public async Task<string> GetSection()
        {
            int merchatnId = _baseHandlerService.MerchantId;
            var rt = await _sectionKeyService.GetByMerchantIdAsync(merchatnId);

            return (rt.Item2.Select(t => new
            {
                t.Id,
                t.SKey,
                DetailType = t.DetailType.ToString(),
                TypeDesc = t.Type.GetDescription(),
                t.Description
            }), rt.Item2.Count()).ToTableModel().ToJson();
        }

        [HttpPost("sectiondic")]
        public async Task<string> GetSectionDic()
        {
            var rt = await _sectionKeyService.GetByMerchantIdAsync(_baseHandlerService.MerchantId);
            var rt2 = rt.Item2.ToDictionary(t => t.Id, t => t.SKey + t.Description);
            return (true, rt2.ToJson()).ToJsonResult();
        }


        [HttpPost("sectiondetails")]
        public async Task<string> GetSectionDetails([FromForm] int sectionId)
        {
            var rt = await _sectionDetailService.GetBySectionIdAsync(_baseHandlerService.MerchantId, sectionId);
            IEnumerable<SectionDetail> secDetails = rt.Item2;
            var secKey = await _sectionKeyService.GetAsync(sectionId);
            if (DefaultString.SubMenuSkey.Contains(secKey))
            {
                // 只显示游戏列表中开启的相关选项
                var sysEnabledGames = await _gameMerchantService.GetListAsync(new MerchantGameListQuery() { Limit = 1000, MerchantId = _baseHandlerService.MerchantId });

                secDetails = from sec in rt.Item2
                             join en in sysEnabledGames.Item1
                             on sec.PageUrl equals en.TypeStr
                             orderby sec.SortNo descending
                             select sec;
            }

            return (true, secDetails.OrderByDescending(x => x.SortNo)).ToJsonResult();
        }

        [HttpPost("sectionsave")]
        public async Task<string> SaveSectionDetails([FromForm] SectionDetailsModifyModel md)
        {
            md.MerchantId = _baseHandlerService.MerchantId;
            return (await _sectionDetailService.InsertOrUpdateAsync(md)).ToJsonResult();
        }

        [HttpPost("updatecontent")]
        public async Task<string> UpdateContent([FromForm] int secId, [FromForm] int dId, [FromForm] string tcontent, [FromForm] string detailType)
        {
            //return (new { id = id, content = tcontent }).ToJson();
            return (await _sectionDetailService.UpdateContentAsync(_baseHandlerService.MerchantId, secId, dId, detailType, tcontent)).ToJsonResult();
        }


        [HttpPost("savebanner")]
        public async Task<string> SaveBanner([FromForm] int secId, [FromForm] List<SectionBanner> banner)
        {
            return (await _sectionDetailService.UpdateBannerAsync(_baseHandlerService.MerchantId, secId, banner)).ToJsonResult();
        }

        #endregion


        #region 区域帮助管理 HelpArea


        [HttpPost("helparealist")]
        public async Task<String> GetHelpAreaListAsync([FromBody] HelpAreaListQuery q)
        {
            q.MerchantId = _baseHandlerService.MerchantId;

            var helpTypeDic = await _helpAreaTypeService.GetDicAsync(q.MerchantId);
            var rt = await _helpAreaService.GetPageListAsync(q);
            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.TypeId,
                    TypeName = helpTypeDic[t.TypeId],
                    t.Title,
                    t.Tcontent,
                    t.ShowIndexPage,
                    t.Alias,
                    t.SortNo,
                    t.CreateTime
                })
            }).ToJson();

        }

        [HttpPost("helptypearealist")]
        public async Task<string> GetHelpAreaTypeListAsync()
        {
            int merchatnId = _baseHandlerService.MerchantId;
            var rt = await _helpAreaTypeService.GetPageListAsync(merchatnId);
            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.IsHref,
                    t.IsOpen,
                    t.Href,
                    t.CreateTime
                })
            }).ToJson();
        }

        [HttpPost("helptypedic")]
        public async Task<string> GetHelpAreaTypeDicAsync()
        {
            int merchantId = _baseHandlerService.MerchantId;
            return (true, (await _helpAreaTypeService.GetDicAsync(merchantId)).ToJson()).ToJsonResult();
        }

        [HttpPost("savehelptype")]
        public async Task<string> SaveHelpAreaTypeAsync([FromForm] HelpAreaTypeAOM aom)
        {
            aom.MerchantId = _baseHandlerService.MerchantId;
            return (await _helpAreaTypeService.InsertOrModifyAsync(aom)).ToJsonResult();
        }


        [HttpPost("savehelpcontent")]
        public async Task<string> SaveHelpAreaContent([FromForm] HelpAreaAOM m)
        {
            m.MerchantId = _baseHandlerService.MerchantId;

            return (await _helpAreaService.InsertOrModify(m)).ToJsonResult();
        }


        [HttpPost("gethelpcontent")]
        public async Task<string> GetHelpAreaById([FromForm] int id)
        {
            int merchantId = _baseHandlerService.MerchantId;
            return (await _helpAreaService.GetAsync(merchantId, id)).ToJsonResult();
        }

        [HttpPost("delhelpcontent")]
        public async Task<string> DeleteHelpAreaContent([FromBody] IdentityBase m)
        {
            int merchantId = _baseHandlerService.MerchantId;
            var cc = await _helpAreaService.DeleteAsync(merchantId, m.Id);
            return cc.ToJsonResult();
        }


        #endregion


        #region 通知公告管理 Notice

        [HttpPost("noticeload")]
        public async Task<string> GetNoticeListAsync([FromForm] NoticeAreaListQuery q)
        {
            q.MerchantId = _baseHandlerService.MerchantId;
            q.Type = null;
            var rt = _noticeAreaService.GetPageList(q);
            return (new TableDataModel()
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Description,
                    t.CreateTime,
                    TypeDes = t.Type.GetDescription(),
                    t.Type,
                    TypeStr = t.Type.ToString(),
                    t.GroupId,
                    t.IsDisplay
                })
            }).ToJson();
        }

        [HttpPost("noticesave")]
        public async Task<string> SaveNoticeAsync([FromForm] NoticeAreaInsertOrModifyModel m)
        {
            m.MerchantId = _baseHandlerService.MerchantId;
            var rt = await _noticeAreaService.SaveAsync(m);
            if (!rt.Item1) return (false, rt.Item2).ToJsonResult();
            int orderId = rt.Item3;

            if (m.Type == NoticeArea.NoticeType.Notice) // 只有用户的通知保存到了NoticeInfo，公告没有
            {
                // todo Group 转换为会员ID
                var listGroups = m.GroupId.Split(',').ToIntArray();
                var listGroupIds = await _usersService.GetIds(_baseHandlerService.MerchantId, listGroups.ToList());

                await _noticeInfoService.DeleteAsync(_baseHandlerService.MerchantId, orderId);
                await _noticeInfoService.Save(_baseHandlerService.MerchantId, listGroupIds, orderId, m.Title, m.Description);
            }

            return (true, "保存成功").ToJsonResult();
        }

        [HttpPost("noticedel")]
        public async Task<string> DeleteNoteceAsync([FromBody] IdentityBase m)
        {
            var rt = await _noticeAreaService.UpdateDeletedStatusAsync(_baseHandlerService.MerchantId, m.Id);
            if (!rt.Item1) return rt.ToJsonResult();
            var rt2 = await _noticeInfoService.DeleteAsync(_baseHandlerService.MerchantId, m.Id);

            await _helpAreaTypeService.DeleteAsync(_baseHandlerService.MerchantId, m.Id);
            return (rt2, "删除成功").ToJsonResult();
        }



        #endregion


    }
}
