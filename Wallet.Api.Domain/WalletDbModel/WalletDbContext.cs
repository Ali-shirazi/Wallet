using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wallet.Api.Domain.WalletDbModel;

public partial class WalletDbContext : DbContext
{
    public WalletDbContext()
    {
    }

    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblWallet> TblWallets { get; set; }

    public virtual DbSet<TblWalletTransaction> TblWalletTransactions { get; set; }

    public virtual DbSet<TblWalletTransactionType> TblWalletTransactionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=212.23.216.55;Database=SSONEW;User Id=AccSndPublisher;Password=Rhine@Database1404;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblWallet>(entity =>
        {
            entity.ToTable("Tbl_Wallet");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SaveDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblWalletTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tbl_Transaction");

            entity.ToTable("Tbl_Wallet_Transaction");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SaveDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblWalletTransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tbl_TransactionType");

            entity.ToTable("Tbl_Wallet_TransactionType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasDefaultValue(1);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SaveDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
