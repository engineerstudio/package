using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Mapper;
using static Y.Infrastructure.Library.Core.AuthController.Entity.SysMenuAction;

namespace Y.Infrastructure.Library.Core.AuthController.Service
{
    public class SysMenuActionService : ISysMenuActionService
    {
        private readonly ISysMenuActionRepository _repository;

        public SysMenuActionService(ISysMenuActionRepository repository)
        {
            _repository = repository;
        }

        public List<string> GetAuthorizedUrl(int[] ids)
        {
            return _repository.GetAuthorizedUrl(ids).ToList();
        }

        public List<string> GetPublicUrl()
        {
            return _repository.GetPublicUrl().ToList();
        }


        public (bool, string) Insert(MenuActionAddOrModifyModel m)
        {
            // ParentId == 0 公共操作方法
            //if (m.ParentId == 0) return (false, "请选择菜单页面");
            // TODO 判断链接地址是否已经存在，存在则不可以

            var d = ModelToEntity.Mapping<SysMenuAction, MenuActionAddOrModifyModel>(m);
            string code = RandomHelper.GuidTo16String();
            if (code.Substring(0, 1).IsNumeric())
                code = RandomHelper.GuidTo16String();

            d.Code = code;
            if (string.IsNullOrEmpty(d.Url)) d.Url = "";
            var rt = _repository.Insert(d);
            if (rt != null && rt.Value == 0) return (false, "保存失败");

            return (true, "保存成功");
        }

        public (bool, string) Update(MenuActionAddOrModifyModel m)
        {
            if (m.Id == 0) return (false, "标识错误");
            var d = _repository.Get(m.Id);
            d.Name = m.Name;
            d.ParentId = m.ParentId;
            d.Url = m.Url;
            d.ActionType = m.ActionType;
            d.DataType = m.DataType;
            if (string.IsNullOrEmpty(d.Url)) d.Url = "";
            var rt = _repository.Update(d);
            if (rt == 0) return (false, "保存失败");

            return (true, "保存成功");
        }

        public Task<(bool, string)> InsertAsync()
        {
            throw new NotImplementedException();
        }


        public List<SysMenuAction> GetList()
        {
            return _repository.GetList().ToList();
        }


        public List<SysMenuAction> GetList(ActionsDataType dType)
        {
            string conditions = $" WHERE DataType!={dType.GetEnumValue()} ";
            return _repository.GetList(conditions).ToList();
        }


        public (IEnumerable<SysMenuAction>, int) GetPageList(MenuActionsListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = "WHERE 1=1 ";
            if (!string.IsNullOrEmpty(q.Url)) conditions += $" AND Url='{q.Url}' ";
            if (!string.IsNullOrEmpty(q.ActionName))
            {
                conditions += $" AND Name like @Name";
                parms.Add("Name", $"%{q.ActionName}%");
            }

            return (_repository.GetListPaged(q.Page, q.Limit, conditions, "Id desc", parms), _repository.RecordCount());
        }
    }
}