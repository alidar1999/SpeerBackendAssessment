using Library.StockMarketTracker.Business.Wallet;
using Library.StockMarketTracker.Data.Wallet;
using Microsoft.Extensions.DependencyInjection;

namespace Library.StockMarketTracker.Extension
{
    public static class StockMarketExtension
    {
        public static void BindStockMarketLibrary(this IServiceCollection services)
        {
            services.AddScoped<IWallet, UserWallet>()
                    .AddScoped<IWalletData, WalletSupabaseHelper>();
        }
    }
}
