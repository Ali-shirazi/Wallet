using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Services.WalletTransactionTypeService;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Api.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionTypeController : Controller
    {
        private readonly IWalletTransactionTypeService _service;

        public TransactionTypeController(IWalletTransactionTypeService service)
        {
            _service = service;
        }

        [HttpPost("AddWalletTransactionType")]
        public async Task<ActionResult<ResponseDto<int>>> AddWalletTransactionType(WalletTransactionTypeDto dto)
        {
            var result = await _service.Create(dto);

            return result;
        }



        [HttpGet("GetAllWalletTransactionsType")]
        public async Task<ActionResult<ResponseDto<List<WalletTransactionTypeResultDto>>>> GetAllWalletTransactionsType()
        {
            var result = await _service.GetAll();
            return Json(result.Data);
        }

        [HttpPost("UpdateWalletTransactionsType")]
        public async Task<ActionResult<ResponseDto<bool>>> UpdateWalletTransactionsType(WalletTransactionTypeResultDto dto)
        {
            var result = await _service.Update(dto);
            return result;
        }



        [HttpPost("DeleteWalletTransactionType/{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteWalletTransactionType(Guid id)
        {
            var result = await _service.Delete(id);
            return result;
        }



        [HttpGet("GetWalletTransactionTypeById/{id}")]
        public async Task<ActionResult<ResponseDto<WalletTransactionTypeResultDto>>> GetWalletTransactionTypeById(Guid id)
        {
            var result = await _service.GetById(id);

            return result;
        }


        [HttpGet("GetForWallet")]
        public async Task<ActionResult<ResponseDto<List<TransactionTypeForWallet>>>> GetForWallet()
        {
            var result = await _service.GetForWallet();
            return Json(result.Data);
        }
    }
}
