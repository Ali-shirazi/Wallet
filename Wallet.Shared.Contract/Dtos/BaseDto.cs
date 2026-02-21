using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Shared.Contract.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool? IsRemove { get; set; } = false;
        public Guid UserSaver { get; set; }
        public DateTime SaveDate { get; set; } = DateTime.Now;
    
}
}
