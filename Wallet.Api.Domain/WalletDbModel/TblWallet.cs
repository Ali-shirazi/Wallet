using System;
using System.Collections.Generic;

namespace Wallet.Api.Domain.WalletDbModel;

public partial class TblWallet
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid SubSysId { get; set; }

    public double Balance { get; set; }

    public DateTime SaveDate { get; set; }

    public Guid UserSaver { get; set; }
}
