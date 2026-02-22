using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;

namespace Wallet.Api.Application.Services.WalletTransactionService
{
    public interface IWalletTransactionService
    {
        Task<ResponseDto<List<WalletTransactionResultDto>?>> GetAll();
        Task<ResponseDto<int>> Create(WalletTransactionDto transactionDto);
        Task<ResponseDto<bool>> Update(WalletTransactionResultDto transactionResultDto);
        Task<ResponseDto<bool>> Delete(Guid id);
        Task<ResponseDto<WalletTransactionResultDto?>> GetById(Guid id);
        Task<List<WalletTransactionResultDto>> GetByWalletId(Guid walletId);
    }
}
