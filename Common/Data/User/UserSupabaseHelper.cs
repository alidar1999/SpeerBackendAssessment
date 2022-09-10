using System.Data;
using Newtonsoft.Json;
using Client = Supabase.Client;

namespace Common.Data.User
{
    public class UserSupabaseHelper : IUserData
    {
        private readonly Client _client;
        public UserSupabaseHelper(IClient client)
        {
            _client = client.DatabaseClient;
        }
        public async Task<DataTable> GetUserByUsername(string username)
        {
            var response = await _client.From<Models.User>().Filter("Username", Postgrest.Constants.Operator.Equals, username).Get();
            return JsonConvert.DeserializeObject<DataTable>(response.Content);
        }

        public async Task InsertUser(Models.User user)
        {
            await _client.From<Models.User>().Insert(user);
        }
    }
}
