using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Extensions.Options;
using Wallet.Service.Services.AccountServiice;
using Wallet.Shared.Contract.Dtos;
namespace Wallet.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly string _serverName;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;
        public AccountController(
            IAccountService accountService,
            IDNTCaptchaValidatorService validatorService,
            IOptions<DNTCaptchaOptions> captchaOptions)
        {
            _accountService = accountService;
            _validatorService = validatorService;
            _captchaOptions = captchaOptions.Value;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto sup)
        {
            if (ModelState.IsValid)
            {
                if (!_validatorService.HasRequestValidCaptchaEntry())
                {
                    ModelState.AddModelError(
                        _captchaOptions.CaptchaComponent.CaptchaInputName,
                        "لطفا مقدار داخل تصویر را درست وارد کنید"
                    );
                    return View(nameof(Login));
                }
            }

            var data = await _accountService.Login(_serverName, sup);

            if (data == null ||
                data.UserId == Guid.Empty ||
                string.IsNullOrEmpty(data.Token))
            {
                TempData["msg"] = "شماره تلفن همراه یا کلمه عبور صحیح نیست.";
                return RedirectToAction(nameof(Login));
            }

            HttpContext.Session.SetString("_Token", data.Token);
            HttpContext.Session.SetString("_usr", data.UserId.ToString());
            HttpContext.Session.SetString("_role", data.Role.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}