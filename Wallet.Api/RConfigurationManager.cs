namespace Wallet.Api
{
    static class RConfigurationManager
    {

        public static IConfiguration AppSetting { get; }
        static RConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        }
    }
}
