using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wallet.Api;
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

var key = builder.Configuration["JWT:Secret"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
    };
});

builder.Services.AddAuthorization();





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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
