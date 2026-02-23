using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ViewModels.LoginVm
{
    public class LoginResponseVm
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }

        public int Role { get; set; }
    }
}
