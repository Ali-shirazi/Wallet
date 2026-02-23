using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionRepository;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;

namespace Wallet.Api.Application.Services.WalletTransactionService
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly IWalletTransactionRepository _wallettransactionRepository;
        private readonly IMapper _mapper;
        public WalletTransactionService(IWalletTransactionRepository wallettransactionRepository, IMapper mapper)
        {
            _wallettransactionRepository = wallettransactionRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<int>> Create(WalletTransactionDto data)
        {
            try
            {

                var res = new TblWalletTransaction()
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    WalletId = data.WalletId,
                    SaveDate = DateTime.Now,
                    UserSaver = data.UserSaver
                };

                var result = await _wallettransactionRepository.AddAsync(res);
                return new ResponseDto<int> { Data = 1, State = 1, Message = "عملیات با موفقیت انجام شد", };
            }

            catch (Exception)
            {
                return new ResponseDto<int> { Data = 0, State = 1003, Message = "انجام عملیات با خطا مواجه شد", };
            }


        }



        public async Task<ResponseDto<List<WalletTransactionResultDto>?>> GetAll()
        {
            try
            {
                var data = await _wallettransactionRepository.GetAllAsync();

                if (data.Any())
                    {
                    var mappedData = _mapper.Map<List<WalletTransactionResultDto>>(data);
                    return new ResponseDto<List<WalletTransactionResultDto>?>() { Data = mappedData, State = 1, Message = "عملیات با موفقیت انجام شد" };
                }
                return new ResponseDto<List<WalletTransactionResultDto>?>() { Data = new List<WalletTransactionResultDto>(), State = 1003, Message = "خطا در لیست ارسالی " };

            }
            catch (Exception)
            {
                return new ResponseDto<List<WalletTransactionResultDto>?>() { Data = new List<WalletTransactionResultDto>(), State = 1001, Message = "انجام عملیات با خطا مواجه شد " };

            }


        }

        public async Task<ResponseDto<bool>> Update(WalletTransactionResultDto data)
        {
            try
            {
                var wallet = await _wallettransactionRepository.GetByIdAsync(data.Id);

                if (wallet == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };

                }

                wallet.Name = data.Name;
                wallet.SaveDate = DateTime.Now;
                wallet.UserSaver = data.UserSaver;
                wallet.WalletId = data.WalletId;

                await _wallettransactionRepository.UpdateAsync(wallet);

                return new ResponseDto<bool>() { Data = true, State = 1, Message = "عملیات با موفقیت انجام شد " };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>() { Data = false, State = 1001, Message = "انجام عملیات با خطا مواجه شد " };

            }
        }






        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            try
            {
                var foundedId = await _wallettransactionRepository.GetByIdAsync(id);
                if (foundedId == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };
                }
              var data= await _wallettransactionRepository.DeleteAsync(id);

                return new ResponseDto<bool>() { Data = data, State = 1, Message = "عملیات با موفقیت انجام شد " };

            }
            catch (Exception)
            {
                return new ResponseDto<bool>() { Data = false, State = 1001, Message = "انجام عملیات با خطا مواجه شد " };
            }
           
        }


        public async Task<ResponseDto<WalletTransactionResultDto>?> GetById(Guid id)
        {
            try
            {
                var entity = await _wallettransactionRepository.GetByIdAsync(id);

                if (entity == null)
                    return new ResponseDto<WalletTransactionResultDto>() { Data = new WalletTransactionResultDto(), State = 1005, Message = "خطا در اطلاعات ارسالی " };

                var mappedData= _mapper.Map<WalletTransactionResultDto>(entity);
                return new ResponseDto<WalletTransactionResultDto>() { Data = mappedData, State = 1, Message = "عملیات با موفقیت انجام شد " };
            }
            catch (Exception)
            {
                return new ResponseDto<WalletTransactionResultDto>() { Data = new WalletTransactionResultDto(), State = 1001, Message = "انجام عملیات با خطا مواجه شد " };
            }
        }


        public async Task<List<WalletTransactionResultDto>> GetByWalletId(Guid walletId)
        {
            try
            {
                 var data = _wallettransactionRepository.Get(x => x.WalletId == walletId);
                 if (data.Any())
                    return _mapper.Map<List<WalletTransactionResultDto>>(data);
            }
            catch (Exception)
            {
                return [];
            }
            return [];
        }
    }
}
