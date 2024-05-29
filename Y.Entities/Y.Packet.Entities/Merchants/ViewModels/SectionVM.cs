using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    /// <summary>
    /// 输出到前端的Key Section Model
    /// </summary>

    public class SectionVM
    {
        public string Key { get; set; }
        public List<SectionDVM> Details { get; set; }
    }

    public class SectionDVM
    {
        public string Title { get; set; }
        public string Img { get; set; }
        public string PUrl { get; set; }
        /// <summary>
        /// 菜单专用  是否有下级菜单
        /// </summary>
        public bool HasSub { get; set; }
        /// <summary>
        /// 菜单专用 下级菜单key
        /// </summary>
        public string SubKey { get; set; }
        public string Txt { get; set; }
        public int SortNo { get; set; }
        public List<SectionDVM> SubD { get; set; }
    }

    public class SectionDVMV2 : SectionDVM
    {
        public bool ShowTop { get; set; }
    }

    public class SectionH5VM
    {
        public string Key { get; set; }
        public List<SectionDForH5VM> Details { get; set; }
    }

    public class SectionDForH5VM
    {
        public string Title { get; set; }
        public string Img { get; set; }
        /// <summary>
        /// 游戏的 请求参数
        /// </summary>
        public string Page { get; set; }
    }

}
