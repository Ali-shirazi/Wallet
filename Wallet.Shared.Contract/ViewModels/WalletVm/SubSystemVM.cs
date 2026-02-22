using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ViewModels.WalletVm
{
    public class SubSystemVM
    {
        public Guid? SysId { get; set; }
        public string SystemName { get; set; } = null!;
    }
}
