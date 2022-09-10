using Common.Models;
using Library.StockMarketTracker.Data.Wallet;

namespace Library.StockMarketTracker.Business.Wallet
{
    public class UserWallet : IWallet
    {
        private readonly IWalletData _walletData;
        private readonly IUser _userInfo;
        public UserWallet(IWalletData walletData, IUser userInfo)
        {
            _walletData = walletData;
            _userInfo = userInfo;
        }

        public async Task<bool> CreateWallet()
        {
            var result = await GetCurrentWallet();
            if (result != null)
            {
                return false;
            }

            await _walletData.CreateWallet();

            return true;
        }

        public async Task<Models.Wallet> GetCurrentWallet()
        {
            var datatable = await _walletData.GetCurrentWallet();
            if (datatable.Rows.Count > 0)
            {
                return new Models.Wallet
                {
                    WalletId = Convert.ToInt32(datatable.Rows[0]["WalletId"]),
                    UserId = _userInfo.Id,
                    Balance = Convert.ToInt32(datatable.Rows[0]["Balance"])
                };
            }
            return null;
        }

        public async Task<bool> UpdateWallet(Models.Wallet wallet)
        {
            var result = await GetCurrentWallet();
            if (result == null)
            {
                return false;
            }

            result.Balance = wallet.Balance;

            await _walletData.UpdateWallet(result);
            return true;
        }
    }
}
