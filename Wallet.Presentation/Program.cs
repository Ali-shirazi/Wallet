using DNTCaptcha.Core;
using Wallet.Service.Services.TransactionService;
using Wallet.Service.Services.TransactionTypeService;
using Wallet.Service.Services.WalletServices;
using Wallet.Service.Services.AccountServiice;
var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Services
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionTypeService, TransactionTypeService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IAccountService, AccountService>();
// Session
builder.Services.AddSession();

// Captcha 
builder.Services.AddDNTCaptcha(options =>
{
    options
        .UseCookieStorageProvider()
        .AbsoluteExpiration(10)
        .ShowExceptionsInResponse(builder.Environment.IsDevelopment())
        .WithEncryptionKey("This is my secure key!")
        .WithNonceKey("NETESCAPADES_NONCE")
        .Identifier("dnt_Captcha");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();