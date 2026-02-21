using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.Dtos
{
    public class WalletDto : BaseDto
    {
        public Guid SubSysId { get; set; }

        public double Balance { get; set; }
    }
}
