using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Y.Packet.Entities.Promotions.EnumerationType
{

    public enum GameType
    {
        #region ESport

        [ESport]
        [Description("泛亚电竞")]
        ESport_AVIA,

        [ESport]
        [Description("东森电竞")]
        ESport_DS,

        [ESport]
        [Description("IM电竞")]
        ESport_IM,

        [ESport]
        [Description("IA电竞")]
        ESport_IA,

        [ESport]
        [Description("雷火电竞")]
        ESport_TF,



        #endregion

        #region Sport

        [Sport]
        [Description("沙巴体育")]
        Sport_Onebook,

        [Sport]
        [Description("IM体育")]
        Sport_IM,

        [Sport]
        [Description("CMD体育")]
        Sport_CMD,

        [Sport]
        [Description("SBTech体育")]
        Sport_SBTech,

        [Sport]
        [Description("平博体育")]
        Sport_Pinnacle,

        [Sport]
        [Description("UG体育")]
        Sport_UG,

        [Sport]
        [Description("小金体育")]
        Sport_XJ,

        #endregion

        #region Slot

        [Slot]
        [Description("泛亚电竞")]
        Slot_AVIA,

        #endregion

    }

    public class ESportAttribute : Attribute { }
    public class SportAttribute : Attribute { }
    public class SlotAttribute : Attribute { }
}

