using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wallet.Service.Services.TransactionTypeService;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;

namespace Wallet.Presentation.Controllers
{
    public class TransactionTypeController : Controller
    {
        
        private readonly string _serverName;
        private readonly ITransactionTypeService _transactionTypeService;
        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
            _serverName = BaseController.ServerName;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _transactionTypeService.GetAll(_serverName);

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var res = await _transactionTypeService.GetAll(_serverName);
            return View(res);

        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var res = await _transactionTypeService.GetById(_serverName, Id);
            return PartialView(res);
        }
        [HttpGet]
        public async Task<IActionResult> _Update(Guid Id)
        {
            var res = await _transactionTypeService.GetById(_serverName, Id);
            return PartialView(res);
        }
        [HttpGet]
        public async Task<IActionResult> _Create()
        {
            var res = new TransactionTypeVm();
            return PartialView(res);
        }
    

        [HttpPost]
        public async Task<IActionResult> Update(TransactionTypeVm data)
        {
            var res = await _transactionTypeService.Update(_serverName, data);
            return Json(res);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var res = await _transactionTypeService.Delete(_serverName, Id);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransactionTypeVm data)
        {
            data.UserSaver = Guid.NewGuid();
            var res = await _transactionTypeService.Create(_serverName, data);
            return Json(res);
        }

     
    }
}
