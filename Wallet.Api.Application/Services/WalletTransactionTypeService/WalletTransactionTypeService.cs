using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionTypeRepository;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;

namespace Wallet.Api.Application.Services.WalletTransactionTypeService
{
    public class WalletTransactionTypeService : IWalletTransactionTypeService
    {
        private readonly IWalletTransactionTypeRepository _WalletTransactionTypeRepository;
        private readonly IMapper _mapper;
        public WalletTransactionTypeService(IWalletTransactionTypeRepository WalletTransactionTypeRepository, IMapper mapper)
        {
            _WalletTransactionTypeRepository = WalletTransactionTypeRepository;

            _mapper = mapper;
        }
        public async Task<ResponseDto<int>> Create(WalletTransactionTypeDto data)
        {
            try
            {

                var res = new TblWalletTransactionType()
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    SaveDate = DateTime.Now,
                    UserSaver = data.UserSaver,
                };

                var result = await _WalletTransactionTypeRepository.AddAsync(res);
                return new ResponseDto<int> { Data = 1, State = 1, Message = "عملیات با موفقیت انجام شد", };

            }

            catch (Exception)
            {
                return new ResponseDto<int> { Data = 0, State = 1003, Message = "انجام عملیات با خطا مواجه شد", };

            }


        }



        public async Task<ResponseDto<List<WalletTransactionTypeResultDto>?>> GetAll()
        {
            try
            {
                var data = await _WalletTransactionTypeRepository.GetAllAsync();

                if (data.Any())
                { var mappedData = _mapper.Map<List<WalletTransactionTypeResultDto>>(data);
                    return new ResponseDto<List<WalletTransactionTypeResultDto>?>() { Data = mappedData, State = 1, Message = "عملیات با موفقیت انجام شد" };

                }
                return new ResponseDto<List<WalletTransactionTypeResultDto>?>() { Data = new List<WalletTransactionTypeResultDto>(), State = 1003, Message = "خطا در لیست ارسالی " };

            }
            catch (Exception)
            {
                return new ResponseDto<List<WalletTransactionTypeResultDto>?>() { Data = new List<WalletTransactionTypeResultDto>(), State = 1001, Message = "انجام عملیات با خطا مواجه شد " };

            }

        }

        public async Task<ResponseDto<bool>> Update(WalletTransactionTypeResultDto data)
        {
            try
            {
                var wallet = await _WalletTransactionTypeRepository.GetByIdAsync(data.Id);

                if (wallet == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };
                }

                wallet.Name = data.Name;
                wallet.SaveDate = DateTime.Now;
                wallet.UserSaver = data.UserSaver;
                wallet.Id = data.Id;

                await _WalletTransactionTypeRepository.UpdateAsync(wallet);

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
                var foundedId = _WalletTransactionTypeRepository.GetByIdAsync(id);
                if (foundedId == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };
                }
                _WalletTransactionTypeRepository.DeleteAsync(id);
                return new ResponseDto<bool>() { Data = true, State = 1, Message = "عملیات با موفقیت انجام شد " };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>() { Data = false, State = 1001, Message = "انجام عملیات با خطا مواجه شد " };

            }
        }


        public async Task<ResponseDto<WalletTransactionTypeResultDto?>> GetById(Guid id)
        {
            try
            {
                var entity = await _WalletTransactionTypeRepository.GetByIdAsync(id);

                if (entity == null)
                { return new ResponseDto<WalletTransactionTypeResultDto?>() { Data = new WalletTransactionTypeResultDto(), State = 1005, Message = "خطا در اطلاعات ارسالی " }; }


                var mappedData= _mapper.Map<WalletTransactionTypeResultDto>(entity);
                return new ResponseDto<WalletTransactionTypeResultDto?>() { Data = mappedData, State = 1, Message = "عملیات با موفقیت انجام شد " };

            }
            catch (Exception)
            {
                return new ResponseDto<WalletTransactionTypeResultDto?>() { Data = new WalletTransactionTypeResultDto(), State = 1001, Message = "انجام عملیات با خطا مواجه شد " };
            }
        }
        public async Task<List<TransactionTypeForWallet>> GetForWallet()
        {
            try
            {
                var data = await _WalletTransactionTypeRepository.GetAllAsync();

                var result = data.Select(x => new TransactionTypeForWallet
                {
                    Code = x.Code,
                    Name = x.Name
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return [];
            }
           
        }
    }
}
