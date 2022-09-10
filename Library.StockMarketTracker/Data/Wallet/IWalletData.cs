using System.Data;

namespace Library.StockMarketTracker.Data.Wallet
{
    public interface IWalletData
    {
        public Task UpdateWallet(Models.Wallet wallet);
        public Task CreateWallet();
        public Task<DataTable> GetCurrentWallet();
    }
}
