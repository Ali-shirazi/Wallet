using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.Dtos
{
    public class CreateTransactionDto
    {
        public Guid WalletId { get; set; }
        public float Amount { get; set; }
        public int TransactionTypeCode { get; set; }
    }
}

