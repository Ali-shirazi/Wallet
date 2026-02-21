using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;


namespace Wallet.Api.Application.Services.WalletService
{
    public interface IWalletService
    {
        Task<List<WalletResultDto>?> GetAll();
        Task<int> Create(WalletDto walletDto);
        Task<bool> Update(WalletResultDto walletResultDto);
        Task<bool> Delete(Guid id);

        Task<WalletResultDto?> GetById(Guid id);
        Task<bool> CreateTransaction(CreateWalletTransactionDto dto);
    }
}
