using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games.ViewModels
{
    public class TransferLogQuery : PageModel
    {
        public string GameType { get; set; }

        public int MemberId { get; set; }
        public int MerchantId { get; set; }
    }
}
