using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ResultDtos
{
    public class ResponseDto
    {
        public string Message { get; set; } = "";
        public int State { get; set; } = 0;


    }

    public class ResponseDto<T> : ResponseDto
    {
        public T? Data {  get; set; }
        public static  implicit operator ResponseDto<T>(int V)
        {
            throw new NotImplementedException();
        }
    }
}
