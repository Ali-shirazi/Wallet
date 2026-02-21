using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.TransactionTypeService
{
    public interface ITransactionTypeService
    {
        Task<List<TransactionTypeVm>> GetAll(string _serverName);
        Task<TransactionTypeVm> GetById(string serverName, Guid Id);
        Task<int> Create(string serverName, TransactionTypeVm data);
        Task<bool> Update(string serverName, TransactionTypeVm data);
        Task<bool> Delete(string serverName, Guid Id);
        Task<List<TransactionTypeForWalletVm>> GetTransactionForWallet(string serverName);
    }
}
