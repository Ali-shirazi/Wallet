using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ResultDtos
{
    public class WalletTransactionResultDto : BaseResultDto
    {

        public Guid WalletId { get; set; }

        public Guid TransactionTypeId { get; set; }

        public double Amount {  get; set; }
   



    }
}
