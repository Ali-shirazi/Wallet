using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Generic;

namespace Wallet.Api.Infrastructure.Repositories.WalletRepository
{
    public class WalletRepository
  : GenericRepository<TblWallet>, IWalletRepository
    {
        public WalletRepository(WalletDbContext context)
            : base(context)
        {
        }
    }
}
