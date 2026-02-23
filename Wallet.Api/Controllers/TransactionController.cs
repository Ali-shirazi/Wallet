using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Services.WalletTransactionService;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly IWalletTransactionService _service;

        public TransactionController(IWalletTransactionService service)
        {
            _service = service;
        }

        [HttpPost("AddTransaction")]
        public async Task<ActionResult<ResponseDto<int>>> AddTransaction(WalletTransactionDto dto)
        {
            var result = await _service.Create(dto);

            return result;
        }



        [HttpGet("GetAllTransactions")]
        public async Task<ActionResult<ResponseDto<List<WalletTransactionResultDto>>>> GetAllTransactions()
        {
            var result = await _service.GetAll();
            return Json(result.Data);
        }

        [HttpPost("UpdateTransaction")]
        public async Task<ActionResult<ResponseDto<bool>>> UpdateTransaction(WalletTransactionResultDto dto)
        {
            var result = await _service.Update(dto);
            return result;
        }



        [HttpPost("DeleteTransactionById/{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteTransactionById(Guid id)
        {
            var result = await _service.Delete(id);
            return result;
        }



        [HttpGet("GetTransactionById/{id}")]
        public async Task<ActionResult<ResponseDto<WalletTransactionResultDto?>>> GetTransactionById(Guid id)
        {
            var result = await _service.GetById(id);

            return result;
        }

        [HttpGet("GetTransationByWalletId/{walletId}")]
        public async Task<ActionResult<ResponseDto<List<WalletTransactionResultDto>>>> GetTransationByWalletId(Guid walletId)
        {
            var result = await _service.GetByWalletId(walletId);
            return Json(result.Data);
        }
    }
}


