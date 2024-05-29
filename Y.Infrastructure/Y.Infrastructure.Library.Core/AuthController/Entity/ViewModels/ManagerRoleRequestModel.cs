using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public class ManagerRoleRequestModel : PageModel
    {
        public int MerchantId { get; set; }
        public string UniqueStr { get; set; }
    }
}