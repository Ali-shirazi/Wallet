using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wallet.Service.Services.TransactionService;
using Wallet.Shared.Contract.ViewModels.TransactionVm;

namespace Wallet.Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly string _serverName;
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _serverName = BaseController.ServerName;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _transactionService.GetAll(_serverName);

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var res = await _transactionService.GetAll(_serverName);
            return View(res);

        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var res = await _transactionService.GetById(_serverName, Id);
            return PartialView(res);
        }
        [HttpGet]
        public async Task<IActionResult> _Update(Guid Id)
        {
            var res = await _transactionService.GetById(_serverName, Id);
            return PartialView(res);
        }
        [HttpGet]
        public async Task<IActionResult> _Create()
        {
            var res = new TransactionVm();
            return PartialView(res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TransactionVm data)
        {
            var res = await _transactionService.Update(_serverName, data);
            return Json(res);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var res = await _transactionService.Delete(_serverName, Id);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransactionVm data)
        {
            data.UserSaver = Guid.NewGuid();
            var res = await _transactionService.Create(_serverName, data);
            return Json(res);
        }


        [HttpGet]
        public async Task<IActionResult> GetTransactionByWalletId(Guid Id)
        {
            var res = await _transactionService.GetTransactionByWalletId(_serverName, Id);
            return PartialView(res);
        }
    }
}
