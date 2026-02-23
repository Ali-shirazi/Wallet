using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wallet.Service.Services.TransactionService;
using Wallet.Service.Services.TransactionTypeService;
using Wallet.Service.Services.WalletServices;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Presentation.Controllers
{
    public class WalletController : Controller
    {
      
        private readonly string _serverName;
        private readonly IWalletService _WalletService;
        private readonly ITransactionTypeService _TransactionTypeService;
        private readonly ITransactionService _transactionService;

        public WalletController(IWalletService WalletService, ITransactionService transactionService,ITransactionTypeService transactionTypeService)
        {
            _WalletService = WalletService;
            _transactionService = transactionService;
            _TransactionTypeService= transactionTypeService;
            _serverName = BaseController.ServerName;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _WalletService.GetAll(_serverName);

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var res = await _WalletService.GetAll(_serverName);
            return View(res);

        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var res = await _WalletService.GetById(_serverName, Id);
            return PartialView(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionById(Guid Id)
        {
            var res = await _transactionService.GetById(_serverName, Id);
            return PartialView(res);
        }
        [HttpGet]
        public async Task<IActionResult> _Update(Guid Id)
        {
            var res = await _WalletService.GetById(_serverName, Id);
            return PartialView(res);
        }
        [HttpGet]
        public async Task<IActionResult> _Create()
        {

            var res = await _WalletService.GetAllSubSystem(_serverName);

            // اصلاحیه: تغییر "Title" به "Name" برای تطابق با خروجی JSON
            var subSystemList = new SelectList(res, "Id", "Name");

            ViewBag.SubSystemList = subSystemList;
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> _Sharj(Guid walletId)
        {
          var res = await _TransactionTypeService.GetTransactionForWallet(_serverName);
            var mylist = new List<TransactionTypeForWalletVm>();

            ViewBag.WIdd = walletId;
            return PartialView(res);

          
        }
        public async Task<IActionResult> _Transactionwithdrawal(Guid walletId)
        {
            var res = await _WalletService.GetAllSubSystem(_serverName);
            // اصلاحیه: استفاده از "Name" به جای "Title" یا "SystemName"
            var subSystemList = new SelectList(res, "Id", "Name");
            ViewBag.SubSystemList = subSystemList;
            return PartialView();


        }
        [HttpPost]
        public async Task<IActionResult> Transactionwithdrawal(CreateWalletTransactionDto data)
        {
            data.UserSaver = Guid.NewGuid();
            var res = await _WalletService.Transactionwithdrawal(_serverName, data);
            return Json(res);
        }


      

        [HttpPost]
        public async Task<IActionResult> Update(WalletVm data)
        {
            var res = await _WalletService.Update(_serverName, data);
            return Json(res);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var res = await _WalletService.Delete(_serverName, Id);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(WalletVm data)
        {
            data.UserSaver= Guid.NewGuid();
            var res = await _WalletService.Create(_serverName, data);
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> Sharj(CreateWalletTransactionDto data)
        {
            data.UserSaver = Guid.NewGuid();
            var res = await _WalletService.CreateTransaction(_serverName,data);
            return Json(res);
        }
    }
}
