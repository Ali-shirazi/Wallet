using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Generic;

namespace Wallet.Api.Infrastructure.Repositories.WalletTransactionRepository
{
    public class WalletTransactionRepository
     : GenericRepository<TblWalletTransaction>, IWalletTransactionRepository
    {
        public WalletTransactionRepository(WalletDbContext context)
            : base(context)
        {
        }
    }
}
