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
        public async Task<int> Create(WalletTransactionTypeDto data)
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
                return result;
            }

            catch (Exception)
            {

            }

            return 0;

        }



        public async Task<List<WalletTransactionTypeResultDto>?> GetAll()
        {
            try
            {
                var data = await _WalletTransactionTypeRepository.GetAllAsync();

                if (data.Any())
                    return _mapper.Map<List<WalletTransactionTypeResultDto>>(data);
            }
            catch (Exception)
            {

            }

            return [];
        }

        public async Task<bool> Update(WalletTransactionTypeResultDto data)
        {
            try
            {
                var wallet = await _WalletTransactionTypeRepository.GetByIdAsync(data.Id);

                if (wallet == null)
                    return false;

                wallet.Name = data.Name;
                wallet.SaveDate = DateTime.Now;
                wallet.UserSaver = data.UserSaver;
                wallet.Id = data.Id;

                await _WalletTransactionTypeRepository.UpdateAsync(wallet);

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

                return await _WalletTransactionTypeRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

            }
            return false;
        }


        public async Task<WalletTransactionTypeResultDto?> GetById(Guid id)
        {
            try
            {
                var entity = await _WalletTransactionTypeRepository.GetByIdAsync(id);

                if (entity == null)
                    return null;

                return _mapper.Map<WalletTransactionTypeResultDto>(entity);
            }
            catch (Exception)
            {
                throw new Exception("null");
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
