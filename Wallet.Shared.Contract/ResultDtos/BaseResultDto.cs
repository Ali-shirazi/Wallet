using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.ResultDtos
{
    public class BaseResultDto
    {
        public Guid Id { get; set; }
        [DisplayName("نام")]
        public string? Name { get; set; }
        public bool IsRemove { get; set; }
        public Guid UserSaver { get; set; }
        public DateTime SaveDate { get; set; }

    }
}
