using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Services.AccountService;
using Wallet.Shared.Contract.Dtos;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IConfiguration _configuration;
        string serverName;

        public AccountController(IAccountService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
            serverName = _configuration["AuthServiceInfo:Server"]!;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto)
        {
            var result = await _service.Login(serverName, dto);
            return Ok(result);
        }
    }
}