using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.ViewModel.BootstrapTable
{
    public class BsTable
    {
        public int total { get; set; }
        public IEnumerable<dynamic> rows { get; set; }
    }
}