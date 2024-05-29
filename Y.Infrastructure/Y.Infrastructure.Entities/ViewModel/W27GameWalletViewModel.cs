using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Entities.ViewModel
{
    public class W27GameTypeWalletViewModel
    {

        public string Cate { get; set; }
        public string Icon { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public List<W27GameDWalletViewModel> Games { get; set; }

    }

    public class W27GameDWalletViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public string Icon { get; set; }

    }
}
