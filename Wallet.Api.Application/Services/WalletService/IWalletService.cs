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
        Task<ResponseDto<List<WalletResultDto>>?> GetAll();
        Task<ResponseDto<int>> Create(WalletDto walletDto);
        Task<ResponseDto<bool>> Update(WalletResultDto walletResultDto);
        Task<ResponseDto<bool>> Delete(Guid id);

        Task<ResponseDto<WalletResultDto>?> GetById(Guid id);
        Task<ResponseDto<bool>> CreateTransaction(CreateWalletTransactionDto dto);
        Task<ResponseDto<bool>> Transactionwithdrawal(CreateWalletTransactionDto dto);
        Task<ResponseDto<List<SubSystemVM>>> GetAllSubSys(string serverName);
    }
}
