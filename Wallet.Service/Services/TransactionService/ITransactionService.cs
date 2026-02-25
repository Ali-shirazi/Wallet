using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.TransactionVm;

namespace Wallet.Service.Services.TransactionService
{
    public interface ITransactionService
    {

        Task<ResponseDto<List<TransactionVm>>> GetAll(string token,string _serverName);

        Task<ResponseDto<TransactionVm>> GetById(string serverName, Guid Id);

        Task<ResponseDto<int>> Create(string serverName, TransactionVm data);

        Task<ResponseDto<bool>> Update(string serverName, TransactionVm data);

        Task<ResponseDto<bool>> Delete(string serverName, Guid Id);

        Task<ResponseDto<List<TransactionVm>>> GetTransactionByWalletId(string serverName, Guid Id);
    }
}
