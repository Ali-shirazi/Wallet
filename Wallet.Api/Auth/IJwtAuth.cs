namespace Wallet.Api.Auth
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password, string sysId, Guid userId);

    }
}
