using System;
using static Y.Infrastructure.Library.Core.AuthController.Entity.SysMenuAction;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public class MenuActionAddOrModifyModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public Int32 ParentId { get; set; }

        public String Name { get; set; }
        public String Url { get; set; }

        public ActionsType ActionType { get; set; }

        public ActionsDataType DataType { get; set; }
    }
}