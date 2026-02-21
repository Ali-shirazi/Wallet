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
        Task<List<WalletTransactionTypeResultDto>?> GetAll();
        Task<int> Create(WalletTransactionTypeDto transactionDto);
        Task<bool> Update(WalletTransactionTypeResultDto transactionResultDto);
        Task<bool> Delete(Guid id);
        Task<WalletTransactionTypeResultDto?> GetById(Guid id);
        Task<List<TransactionTypeForWallet>> GetForWallet();
    }
}
