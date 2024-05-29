using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Packet.Entities.Vips.ViewModels;

namespace Y.Packet.Entities.Vips
{
    /// <summary>
    /// Aaron
    /// 2020-09-17 23:49:49
    /// 
    /// </summary>
    public partial class VipGroups
    {
        [NotMapped]
        public GroupSetting GroupSettingModel
        {
            get
            {
                if (string.IsNullOrEmpty(this.GroupSetting)) return new GroupSetting();
                return JsonConvert.DeserializeObject<GroupSetting>(this.GroupSetting);
            }
        }




    }
}
