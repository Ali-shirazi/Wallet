using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Repositories.WalletRepository;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionRepository;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionTypeRepository;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.WalletTransaction;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Wallet.Api.Application.Services.WalletService
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletTransactionRepository _wallettransactionRepository;
        private readonly IWalletTransactionTypeRepository _walletTransactionTypeRepository;
        readonly HttpClient _client = new HttpClient();
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
        public async Task<List<SubSystemVM>> GetAllSubSys(string serverName)
        {
            try
            {
                _client.BaseAddress = new Uri(serverName);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // نکته: آدرس را طبق بحث قبلی اصلاح کنید (مثلا SubSystem/GetAll)
                var response = await _client.GetAsync("SubSystem/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // چک کردن اینکه محتوا خالی نیست
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return new List<SubSystemVM>(); // برگرداندن لیست خالی به جای خطا
                    }

                    // اگر سرویس SSO لیست خالی برمی‌گرداند، نباید اینجا کرش کند
                    return JsonConvert.DeserializeObject<List<SubSystemVM>>(responseContent) ?? new List<SubSystemVM>();
                }
                else
                {
                    // لاگ گرفتن خطا برای دیباگ
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error calling SSO: {response.StatusCode} - {errorContent}");
                    return new List<SubSystemVM>(); // برگرداندن لیست خالی
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<SubSystemVM>(); // جلوگیری از کرش کردن کل برنامه
            }
        }


        public async Task<ResponseDto<bool>> CreateTransaction(CreateWalletTransactionDto dto)
            {
                 try
                 {
                     // 1. دریافت نوع تراکنش
                     var transType = _walletTransactionTypeRepository
                         .Get(x => x.Code == dto.TransactionTypeCode)
                         .FirstOrDefault();
                 
                     if (transType == null)
                     {
                         return new ResponseDto<bool>
                         {
                             Data = false,
                             State = 1005,
                             Message = "نوع تراکنش یافت نشد"
                         };
                     }
                 
                     // 2. دریافت کیف پول
                     var wallet = await _walletRepository.GetByIdAsync(dto.WalletId);
                 
                     if (wallet == null)
                     {
                         return new ResponseDto<bool>
                         {
                             Data = false,
                             State = 1005,
                             Message = "کیف پول یافت نشد"
                         };
                     }
                 
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
                         {
                             return new ResponseDto<bool>
                             {
                                 Data = false,
                                 State = 1006,
                                 Message = "موجودی کافی نیست"
                             };
                         }
                 
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
                 
                     // 5. بروزرسانی کیف پول
                     wallet.Balance = newBalance;
                     wallet.SaveDate = DateTime.Now;
                 
                     await _walletRepository.UpdateAsync(wallet);
                 
                     // 6. ذخیره نهایی
                     await _walletRepository.SaveChangesAsync();
                 
                     return new ResponseDto<bool>
                     {
                         Data = true,
                         State = 1,
                         Message = "عملیات با موفقیت انجام شد"
                     };
                 }
                 catch (Exception)
                 {
                     return new ResponseDto<bool>
                     {
                         Data = false,
                         State = 1001,
                         Message = "انجام عملیات با خطا مواجه شد"
                     };
                 }
            }

        public async Task<ResponseDto<bool>> Transactionwithdrawal(CreateWalletTransactionDto dto)
        {
            try
            {
                // 1. دریافت نوع تراکنش
                var transType = _walletTransactionTypeRepository
                    .Get(x => x.Code == dto.TransactionTypeCode)
                    .FirstOrDefault();
                if (transType == null)
                {
                    return new ResponseDto<bool>
                    {
                        Data = false,
                        State = 1005,
                        Message = "نوع تراکنش یافت نشد"
                    };
                }

                // 2. دریافت کیف پول
                var wallet = await _walletRepository.GetByIdAsync(dto.WalletId);
                if (wallet == null)
                {
                    return new ResponseDto<bool>
                    {
                        Data = false,
                        State = 1005,
                        Message = "کیف پول یافت نشد"
                    };
                }

                // 3. محاسبه موجودی (فقط افزایش)
                double newBalance = wallet.Balance;
                double amount = dto.Amount;

                // چون این متد برای واریز است، مستقیماً مبلغ را اضافه می‌کنیم
                newBalance -= amount;

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

                // 5. بروزرسانی کیف پول
                wallet.Balance = newBalance;
                wallet.SaveDate = DateTime.Now;
                await _walletRepository.UpdateAsync(wallet);

                // 6. ذخیره نهایی
                await _walletRepository.SaveChangesAsync();

                return new ResponseDto<bool>
                {
                    Data = true,
                    State = 1,
                    Message = "عملیات واریز با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    State = 1001,
                    Message = "انجام عملیات با خطا مواجه شد"
                };
            }
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
                var foundedId = await _walletRepository.GetByIdAsync(id);
                if (foundedId == null)
                {
                    return new ResponseDto<bool>() { Data = false, State = 1005, Message = "خطا در اطلاعات ارسالی " };
                }

                var data = await _walletRepository.DeleteAsync(id);

                return new ResponseDto<bool>() { Data = data, State = 1, Message = "عملیات با موفقیت انجام شد " }; 
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





