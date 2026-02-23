using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Services.AccountService;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.LoginVm;

namespace Wallet.Api.Controllers
{
    public class AccountController(IAccountService _service,IConfiguration _configuration) : Controller
    {
        string serverName = _configuration["AuthServiceInfo:Server"]!;


        [HttpGet("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto)
        {
            var result = await _service.Login(serverName, dto);
            return Json(result);
        }
    }
}
