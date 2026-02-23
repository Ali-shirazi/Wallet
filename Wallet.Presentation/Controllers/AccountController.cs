using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Wallet.Service.Services.AccountServiice;
using Wallet.Shared.Contract.Dtos;

namespace Wallet.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly string _serverName;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            _serverName = BaseController.ServerName;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto data)
        {
            var res = _accountService.Login(_serverName, data);
            return Json(res);
        }
    }
}
