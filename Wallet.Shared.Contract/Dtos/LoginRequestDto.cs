using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.Dtos
{
    public class LoginRequestDto
    {
        public string Mobile {  get; set; }
        public string Password { get; set; }
        public Guid SystemId {  get; set; }
    }
}
