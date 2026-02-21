using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Shared.Contract.Dtos;
using Wallet.Shared.Contract.ResultDtos;
using Wallet.Shared.Contract.ViewModels.TransactionTypeVm;
using Wallet.Shared.Contract.ViewModels.TransactionVm;
using Wallet.Shared.Contract.ViewModels.WalletVm;
using Wallet.Shared.Contract.WalletTransaction;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wallet.Service.Auto_Mappers
{
    public class Auto_Mappers : Profile
    {
        public Auto_Mappers()
        {
            CreateMap<WalletTransactionResultDto, TransactionTypeVm>().ReverseMap();
            CreateMap<WalletTransactionResultDto, TransactionVm>().ReverseMap();
            CreateMap<WalletResultDto, WalletVm>().ReverseMap();
            CreateMap<TransactionTypeForWallet, TransactionTypeForWalletVm>().ReverseMap();
            CreateMap<WalletTransactionResultDto, TransactionVm>().ReverseMap();

            //CreateMap<CreateTransactionDto, CreateWalletTransactionDto>().ReverseMap();

        }
    }
    
}
