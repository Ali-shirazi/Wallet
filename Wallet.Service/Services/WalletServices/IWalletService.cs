using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.WalletServices
{
    public interface IWalletService
    {
        Task<List<WalletVm>> GetAll(string _serverName);
        Task<WalletVm> GetById(string serverName, Guid Id);
        Task<int> Create(string serverName, WalletVm data);
        Task<bool> Update(string serverName, WalletVm data);
        Task<bool> Delete(string serverName, Guid Id);
        Task<bool> CreateTransaction(string serverName, CreateWalletTransactionDto data);
        Task<bool> Transactionwithdrawal(string serverName, CreateWalletTransactionDto data);
        Task<List<SubSystemVM>> GetAllSubSystem(string serverName);

    }
}
