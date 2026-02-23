using Microsoft.EntityFrameworkCore;
using Wallet.Api.Application.MappingProfille;
using Wallet.Api.Application.Services.AccountService;
using Wallet.Api.Application.Services.WalletService;
using Wallet.Api.Application.Services.WalletTransactionService;
using Wallet.Api.Application.Services.WalletTransactionTypeService;
using Wallet.Api.Domain.WalletDbModel;
using Wallet.Api.Infrastructure.Repositories.WalletRepository;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionRepository;
using Wallet.Api.Infrastructure.Repositories.WalletTransactionTypeRepository;
//using Wallet.Api.Infrastructure.Wallet.Api.Domain.WalletDbModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()        // ????? ?????? ?? ??? ???????
              .AllowAnyMethod()        // ????? ??? ????? (GET, POST, ...)
              .AllowAnyHeader();       // ????? ??? ?????
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
});


builder.Services.AddDbContext<WalletDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("WalletDb")
    ));

builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
builder.Services.AddScoped<IWalletTransactionService, WalletTransactionService>();

builder.Services.AddScoped<IWalletTransactionTypeRepository, WalletTransactionTypeRepository>();
builder.Services.AddScoped<IWalletTransactionTypeService, WalletTransactionTypeService>();
builder.Services.AddScoped<IAccountService, AccountService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
