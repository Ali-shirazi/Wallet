using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ViewModels.TransactionTypeVm
{
    public class TransactionTypeVm
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime SaveDate { get; set; }
        public Guid UserSaver { get; set; }
    }
}
