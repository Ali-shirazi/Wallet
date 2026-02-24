using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.WalletServices
{
    public interface IWalletService
    {
        Task<ResponseDto<List<WalletVm>>> GetAll(string _serverName);
        Task<ResponseDto<WalletVm>> GetById(string serverName, Guid Id);
        Task<ResponseDto<int>> Create(string serverName, WalletVm data);
        Task<ResponseDto<bool>> Update(string serverName, WalletVm data);
        Task<ResponseDto<bool>> Delete(string serverName, Guid Id);
        Task<ResponseDto<bool>> CreateTransaction(string serverName, CreateWalletTransactionDto data);
        Task<ResponseDto<bool>> Transactionwithdrawal(string serverName, CreateWalletTransactionDto data);
        Task<ResponseDto<List<SubSysVM>>> GetAllSubSystem(string serverName); // تغییر یافته

    }
}
