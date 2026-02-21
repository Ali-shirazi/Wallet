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
        Task<List<WalletTransactionResultDto>?> GetAll();
        Task<int> Create(WalletTransactionDto transactionDto);
        Task<bool> Update(WalletTransactionResultDto transactionResultDto);
        Task<bool> Delete(Guid id);
        Task<WalletTransactionResultDto?> GetById(Guid id);
        Task<List<WalletTransactionResultDto>?> GetByWalletId(Guid walletId);
    }
}
