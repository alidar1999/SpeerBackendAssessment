namespace Library.StockMarketTracker.Business.Wallet
{
    public interface IWallet
    {
        public Task<Models.Wallet> GetCurrentWallet();
        public Task<bool> UpdateWallet(Models.Wallet wallet);
        public Task<bool> CreateWallet();
    }
}
