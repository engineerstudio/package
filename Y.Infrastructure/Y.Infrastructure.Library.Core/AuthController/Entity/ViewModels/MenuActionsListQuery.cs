using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public class MenuActionsListQuery : PageModel
    {
        public string Url { get; set; }

        public string ActionName { get; set; }
    }
}