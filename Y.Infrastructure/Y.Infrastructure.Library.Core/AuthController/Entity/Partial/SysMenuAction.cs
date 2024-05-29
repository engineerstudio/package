using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public partial class SysMenuAction
    {
        /// <summary>
        /// 数据访问类别
        /// </summary>
        public enum ActionsType
        {
            [Description("公共访问数据")] Public = 1,
            [Description("权限访问数据")] Private = 2
        }

        /// <summary>
        /// 数据类别
        /// </summary>
        public enum ActionsDataType
        {
            [Description("默认")] None = 0,
            [Description("列表")] List = 1,
            [Description("按钮")] Button = 2,
            [Description("弹页面按钮")] PageButton = 3
        }
    }
}