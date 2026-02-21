using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Generic;

namespace Wallet.Api.Infrastructure.Repositories.WalletTransactionTypeRepository
{
    public interface IWalletTransactionTypeRepository
     : IGenericRepository<TblWalletTransactionType>
    {
    }
}
