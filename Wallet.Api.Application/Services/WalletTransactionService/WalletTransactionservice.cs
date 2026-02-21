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
        public async Task<int> Create(WalletTransactionDto data)
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
                return result;
            }

            catch (Exception)
            {

            }

            return 0;

        }



        public async Task<List<WalletTransactionResultDto>?> GetAll()
        {
            try
            {
                var data = await _wallettransactionRepository.GetAllAsync();

                if (data.Any())
                    return _mapper.Map<List<WalletTransactionResultDto>>(data);
            }
            catch (Exception)
            {

            }

            return [];
        }

        public async Task<bool> Update(WalletTransactionResultDto data)
        {
            try
            {
                var wallet = await _wallettransactionRepository.GetByIdAsync(data.Id);

                if (wallet == null)
                    return false;

                wallet.Name = data.Name;
                wallet.SaveDate = DateTime.Now;
                wallet.UserSaver = data.UserSaver;
                wallet.WalletId = data.WalletId;

                await _wallettransactionRepository.UpdateAsync(wallet);

                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }






        public async Task<bool> Delete(Guid id)
        {
            try
            {

                return await _wallettransactionRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

            }
            return false;
        }


        public async Task<WalletTransactionResultDto?> GetById(Guid id)
        {
            try
            {
                var entity = await _wallettransactionRepository.GetByIdAsync(id);

                if (entity == null)
                    return null;

                return _mapper.Map<WalletTransactionResultDto>(entity);
            }
            catch (Exception)
            {
                throw new Exception("null");
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
