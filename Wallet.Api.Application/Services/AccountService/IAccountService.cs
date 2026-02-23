using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;

namespace Wallet.Api.Application.Services.AccountService
{
    public interface IAccountService
    {
        Task<LoginResponseDto> Login(string serverName, LoginRequestDto dto);
    }
}
