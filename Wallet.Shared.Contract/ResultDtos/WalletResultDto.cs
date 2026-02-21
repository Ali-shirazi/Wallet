using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ResultDtos
{
    public class WalletResultDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid SubSysId { get; set; }
        public float Balance { get; set; }
        public DateTime SaveDate { get; set; }
        public Guid UserSaver { get; set; }
    }
}
