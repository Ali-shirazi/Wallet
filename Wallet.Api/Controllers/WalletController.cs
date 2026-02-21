using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Services.WalletService;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController(IWalletService _service) : Controller
    {


        [HttpPost("AddWallet")]
        public async Task<ActionResult<int>> AddWallet(WalletDto dto)
        {
            var result = await _service.Create(dto);

            return Json(result);
        }

        [HttpGet("GetAllWallets")]
        public async Task<ActionResult<List<WalletResultDto>>> GetAllWallets()
        {
            var result = await _service.GetAll();
            return Json(result);
        }
        [HttpPost("UpdateWallet")]
        public async Task<ActionResult<bool>> UpdateWallet(WalletResultDto dto)
        {
            var result = await _service.Update(dto);
            return Json(result);
        }
        [HttpPost("DeleteWallet/{id}")]
        public async Task<ActionResult<bool>> DeleteWallet(Guid id)
        {

            var result = await _service.Delete(id);
            return Json(result);
        }

        [HttpGet("GetByIdWallet/{id}")]
        public async Task<ActionResult<WalletResultDto>> GetByIdWallet(Guid id)
        {
            var result = await _service.GetById(id);

            if (result == null)
                return NotFound();

            return Json(result);
        }
        [HttpPost("CreateTransaction")]
        public async Task<ActionResult<bool>> CreateTransaction(CreateWalletTransactionDto dto)
        {
            var result = await _service.CreateTransaction(dto);
            return Json(result);
        }
    }
}
