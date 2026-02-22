using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // این فضای نام را اضافه کنید
using Wallet.Api.Application.Services.WalletService;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController(IWalletService _service, IConfiguration _configuration) : Controller
    {
        // اضافه کردن IConfiguration به سازنده کلاس (در بالا انجام شد)

        [HttpPost("AddWallet")]
        public async Task<ActionResult<ResponseDto<int>>> AddWallet(WalletDto dto)
        {
            var result = await _service.Create(dto);
            return Json(result.Data);
        }

        [HttpGet("GetAllWallets")]
        public async Task<ActionResult<ResponseDto<List<WalletResultDto>>>> GetAllWallets()
        {
            var result = await _service.GetAll();
            return Json(result.Data);
        }

        [HttpPost("UpdateWallet")]
        public async Task<ActionResult<ResponseDto<bool>>> UpdateWallet(WalletResultDto dto)
        {
            var result = await _service.Update(dto);
            return Json(result.Data);
        }

        [HttpPost("DeleteWallet/{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteWallet(Guid id)
        {
            var result = await _service.Delete(id);
            return Json(result.Data);
        }

        [HttpGet("GetByIdWallet/{id}")]
        public async Task<ActionResult<ResponseDto<WalletResultDto>>> GetByIdWallet(Guid id)
        {
            var result = await _service.GetById(id);
            return Json(result.Data);
        }

        [HttpPost("CreateTransaction")]
        public async Task<ActionResult<ResponseDto<bool>>> CreateTransaction(CreateWalletTransactionDto dto)
        {
            var result = await _service.CreateTransaction(dto);
            return Json(result);
        }

        [HttpPost("Transactionwithdrawal")]
        public async Task<ActionResult<ResponseDto<bool>>> Transactionwithdrawal(CreateWalletTransactionDto dto)
        {
            var result = await _service.Transactionwithdrawal(dto);
            return Json(result);
        }

        [HttpGet("GetAllSubSys")]
        public async Task<ActionResult<ResponseDto<List<SubSystemVM>>>> GetAllSubSys()
        {
            
            string serverName = _configuration["AuthServiceInfo:Server"]!;

            // ارسال آدرس به متد سرویس
            var result = await _service.GetAllSubSys(serverName);

            return Json(result);
        }
    }
}