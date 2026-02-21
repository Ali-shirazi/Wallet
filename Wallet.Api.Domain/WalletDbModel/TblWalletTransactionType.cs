using System;
using System.Collections.Generic;

namespace Wallet.Api.Domain.WalletDbModel;

public partial class TblWalletTransactionType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime SaveDate { get; set; }

    public Guid UserSaver { get; set; }

    public int? Code { get; set; }
}
