using System;
using System.Collections.Generic;

namespace Wallet.Api.Domain.WalletDbModel;

public partial class TblWalletTransaction
{
    public Guid Id { get; set; }

    public Guid WalletId { get; set; }

    public Guid TransactionTypeId { get; set; }

    public string? Name { get; set; }

    public DateTime SaveDate { get; set; }

    public Guid UserSaver { get; set; }

    public double? Amount { get; set; }
}
