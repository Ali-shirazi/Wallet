using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ViewModels.TransactionVm
{
    public class TransactionVm
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public string? Name { get; set; }
        public DateTime SaveDate { get; set; }
        public Guid UserSaver { get; set; }

        public double Amount {  get; set; }
        
    }
}
