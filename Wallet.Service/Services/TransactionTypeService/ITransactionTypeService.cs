using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Service.Services.TransactionTypeService
{
    public interface ITransactionTypeService
    {
        Task<ResponseDto<List<TransactionTypeVm>>> GetAll(string serverName);

        Task<ResponseDto<TransactionTypeVm>> GetById(string serverName, Guid Id);

        Task<ResponseDto<int>> Create(string serverName, TransactionTypeVm data);

        Task<ResponseDto<bool>> Update(string serverName, TransactionTypeVm data);

        Task<ResponseDto<bool>> Delete(string serverName, Guid Id);

        Task<ResponseDto<List<TransactionTypeForWalletVm>>> GetTransactionForWallet(string serverName);
    }
}