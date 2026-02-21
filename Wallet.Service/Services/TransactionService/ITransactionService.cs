using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.ViewModels.TransactionVm;

namespace Wallet.Service.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<List<TransactionVm>> GetAll(string _serverName);
        Task<TransactionVm> GetById(string serverName, Guid Id);
        Task<int> Create(string serverName, TransactionVm data);
        Task<bool> Update(string serverName, TransactionVm data);
        Task<bool> Delete(string serverName, Guid Id);
        Task<List<TransactionVm>> GetTransactionByWalletId(string serverName, Guid Id);
    }
}
