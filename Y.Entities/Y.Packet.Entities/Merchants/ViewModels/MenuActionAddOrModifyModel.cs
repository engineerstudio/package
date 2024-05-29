using System;
using System.Collections.Generic;
using System.Text;
using static Y.Packet.Entities.Merchants.SysMenuAction;

namespace Y.Packet.Entities.Merchants.ViewModels
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
