using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Api.Application.Services.WalletTransactionTypeService
{
    public interface IWalletTransactionTypeService
    {
        Task<ResponseDto<List<WalletTransactionTypeResultDto>?>> GetAll();
        Task<ResponseDto<int> >Create(WalletTransactionTypeDto transactionDto);
        Task<ResponseDto<bool>> Update(WalletTransactionTypeResultDto transactionResultDto);
        Task<ResponseDto<bool>> Delete(Guid id);
        Task<ResponseDto<WalletTransactionTypeResultDto?>> GetById(Guid id);
        Task<List<TransactionTypeForWallet>> GetForWallet();
    }
}
