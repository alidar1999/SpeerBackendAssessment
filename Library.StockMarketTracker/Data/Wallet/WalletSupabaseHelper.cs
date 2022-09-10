using System.Data;
using Common.Models;
using Library.StockMarketTracker.Models;
using Newtonsoft.Json;
using Postgrest;
using Client = Supabase.Client;

namespace Library.StockMarketTracker.Data.Wallet
{
    public class WalletSupabaseHelper : IWalletData
    {
        private readonly IUser _userInfo;
        private readonly Client _client;

        public WalletSupabaseHelper(IUser userInfo)
        {
            _userInfo = userInfo;
            _client = Client.Instance;
        }

        public async Task CreateWallet()
        {
            var wallet = new Models.Wallet();
            wallet.UserId = _userInfo.Id;

            await _client.From<Models.Wallet>().Insert(wallet);
        }

        public async Task<DataTable> GetCurrentWallet()
        {
            var response = await _client.From<Models.Wallet>()
                .Filter("UserId", Constants.Operator.Equals, _userInfo.Id)
                .Get();

            return JsonConvert.DeserializeObject<DataTable>(response.Content);
        }

        public async Task UpdateWallet(Models.Wallet wallet)
        {
            await _client.From<Models.Wallet>()
                .Filter("UserId", Constants.Operator.Equals, _userInfo.Id)
                .Update(wallet);
        }
    }
}
