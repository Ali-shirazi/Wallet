using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Repositories.WalletRepository;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionRepository;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionTypeRepository;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;


namespace Wallet.Api.Application.Services.WalletService
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletTransactionRepository _wallettransactionRepository;
        private readonly IWalletTransactionTypeRepository _walletTransactionTypeRepository;
        private readonly IMapper _mapper;

        public WalletService(
            IWalletRepository walletRepository,
            IWalletTransactionRepository wallettransactionRepository,
            IWalletTransactionTypeRepository walletTransactionTypeRepository,
            IMapper mapper)
        {
            _walletRepository = walletRepository;
            _wallettransactionRepository = wallettransactionRepository;
            _walletTransactionTypeRepository = walletTransactionTypeRepository;
            _mapper = mapper;
        }


        public async Task<bool> CreateTransaction(CreateWalletTransactionDto dto)
        {
            //try
            //{

            var transType = _walletTransactionTypeRepository.Get(x => x.Code == dto.TransactionTypeCode).FirstOrDefault();
            //var transType = transTypeList.FirstOrDefault();


            // 2. دریافت کیف پول
            var wallet = await _walletRepository.GetByIdAsync(dto.WalletId);


            // 3. محاسبه موجودی
            double newBalance = wallet.Balance;
            double amount = dto.Amount;

            if (transType.Code == 1) // افزایش
            {
                newBalance += amount;
            }
            else // کاهش
            {
                if (wallet.Balance < amount)
                    throw new Exception("موجودی کافی نیست");
                newBalance -= amount;
            }

            // 4. ثبت تراکنش
            var newTransaction = new TblWalletTransaction()
            {
                Id = Guid.NewGuid(),
                WalletId = dto.WalletId,
                TransactionTypeId = transType.Id,
                Name = transType.Name,
                Amount = amount,
                SaveDate = DateTime.Now,
                UserSaver = dto.UserSaver,
            };
            await _wallettransactionRepository.AddAsync(newTransaction);

            wallet.Balance = newBalance;
            wallet.SaveDate = DateTime.Now;
            await _walletRepository.UpdateAsync(wallet);

            // 6. ذخیره نهایی
            await _walletRepository.SaveChangesAsync();

            return true;
            //}
            //catch (Exception)
            //{

            //   return false;
            //}
        }





        public async Task<ResponseDto<int>> Create(WalletDto data)
        {
            try
            {

                var res = new TblWallet()
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    Balance = data.Balance,
                    SubSysId = data.SubSysId,
                    SaveDate = DateTime.Now,
                    UserSaver = data.UserSaver
                };

                var result = await _walletRepository.AddAsync(res);
                return new ResponseDto<int> { Data = 1, State = 1, Message = "عملیات با موفقیت انجام شد", };
            }

            catch (Exception)
            {
                return new ResponseDto<int> { Data = 0, State = 1003, Message = "انجام عملیات با خطا مواجه شد", };
            }
        }



        public async Task<ResponseDto<List<WalletResultDto>>?> GetAll()
        {
            try
            {
                var data = await _walletRepository.GetAllAsync();

                if (data.Any())
                {
                    var mappedData = _mapper.Map<List<WalletResultDto>>(data);

                    return new ResponseDto<List<WalletResultDto>>() { Data = mappedData, State = 1, Message = "عملیات با موفقیت انجام شد" };
                }
                return new ResponseDto<List<WalletResultDto>>() { Data = new List<WalletResultDto>(), State = 1003, Message = "خطا در لیست ارسالی " };
            }
            catch (Exception)
            {
                return new ResponseDto<List<WalletResultDto>>() { Data = new List<WalletResultDto>(), State = 1001, Message = "انجام عملیات با خطا مواجه شد " };
            }

        }

        public async Task<ResponseDto<bool>> Update(WalletResultDto data)
        {
            try
            {
                var wallet = await _walletRepository.GetByIdAsync(data.Id);

                if (wallet == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };

                }
                wallet.Name = data.Name;
                wallet.Balance = data.Balance;
                wallet.SubSysId = data.SubSysId;
                wallet.SaveDate = DateTime.Now;
                wallet.UserSaver = data.UserSaver;

                await _walletRepository.UpdateAsync(wallet);

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
                var foundedId = _walletRepository.GetByIdAsync(id);
                if (foundedId == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };
                }



                return new ResponseDto<bool>() { Data = true, State = 1, Message = "عملیات با موفقیت انجام شد " };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>() { Data = false, State = 1001, Message = "انجام عملیات با خطا مواجه شد " };
            }

        }


        public async Task<ResponseDto<WalletResultDto>?> GetById(Guid id)
        {
            try
            {
                var entity = await _walletRepository.GetByIdAsync(id);

                if (entity == null)
                    return new ResponseDto<WalletResultDto>() { Data = new WalletResultDto(), State = 1005, Message = "خطا در اطلاعات ارسالی " };

                var mappedData = _mapper.Map<WalletResultDto>(entity);
                return new ResponseDto<WalletResultDto>() { Data = mappedData, State = 1, Message = "عملیات با موفقیت انجام شد " };
            }
            catch (Exception)
            {
                return new ResponseDto<WalletResultDto>() { Data = new WalletResultDto(), State = 1001, Message = "انجام عملیات با خطا مواجه شد " };

            }
        }

    }
}





