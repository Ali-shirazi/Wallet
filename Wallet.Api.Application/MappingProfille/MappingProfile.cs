using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;

namespace Wallet.Api.Application.MappingProfille
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TblWallet, WalletResultDto>().ReverseMap();
            CreateMap<TblWallet, WalletDto>().ReverseMap(); 
            CreateMap<TblWalletTransactionType, WalletTransactionTypeResultDto>().ReverseMap();
            CreateMap<TblWalletTransactionType, WalletTransactionTypeDto>().ReverseMap();
            CreateMap<TblWalletTransaction, WalletTransactionDto>().ReverseMap();
            CreateMap<TblWalletTransaction, WalletTransactionResultDto>().ReverseMap();

        }
    }
}
