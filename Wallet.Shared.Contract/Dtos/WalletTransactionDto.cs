using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.Dtos
{
    public class WalletTransactionDto : BaseDto
    {
        public Guid WalletId { get; set; }

        public Guid TransactionTypeId { get; set; }

    }
}
