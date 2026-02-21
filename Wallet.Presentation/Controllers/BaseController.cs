using Microsoft.AspNetCore.Mvc;

namespace Wallet.Presentation.Controllers
{
    public class BaseController : Controller
    {
        internal static readonly string ServerName = ConfigurationManager.AppSetting["ServiceInfo:Server"]!;
        public IActionResult Index()
        {
            return View();
        }
    }
}
