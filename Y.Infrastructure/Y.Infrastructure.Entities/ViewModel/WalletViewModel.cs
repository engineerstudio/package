using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Entities.ViewModel
{
    public class WalletBaseViewModel
    {
        public string CenterWallet { get; set; }
        public string LockdownWallet { get; set; }
    }

    public class WalletViewModel: WalletBaseViewModel
    {
        public IEnumerable<GameWallet> GamesWallet { get; set; }
    }

    public class GameWallet
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }

    public class WalletViewModelV2: WalletBaseViewModel
    {
        public List<GameWalletCate> GameCate { get; set; }
    }

    public class GameWalletCate: GameWallet
    {
        public List<GameWalletV2> GamesWallet { get; set; } = new List<GameWalletV2>();
    }

    public class GameWalletV2
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public string CateType { get; set; }
    }
}
