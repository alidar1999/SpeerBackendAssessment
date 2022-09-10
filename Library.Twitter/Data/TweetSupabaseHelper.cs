using System.Data;
using Common.Models;
using Library.Twitter.Models;
using Newtonsoft.Json;
using Postgrest;
using Client = Supabase.Client;

namespace Library.Twitter.Data
{
    public class TweetSupabaseHelper : ITweetData
    {
        private readonly IUser _userInfo;
        private readonly Client _client;
        public TweetSupabaseHelper(IUser userInfo)
        {
            _userInfo = userInfo;
            _client = Client.Instance;
        }

        public async Task Create(Tweet tweet)
        {
            await _client.From<Tweet>().Insert(tweet);
        }

        public async Task Delete(int tweetId)
        {
            await _client.From<Tweet>()
                .Filter("TweetId", Constants.Operator.Equals, tweetId)
                .Delete();
        }

        public async Task<DataTable> Read(int tweetId)
        {
            var response = await _client.From<Tweet>()
                .Filter("TweetId", Constants.Operator.Equals, tweetId)
                .Get();

            return JsonConvert.DeserializeObject<DataTable>(response.Content);
        }

        public async Task Update(Tweet tweet)
        {
            await _client.From<Tweet>()
                    .Filter("TweetId", Constants.Operator.Equals, tweet.Id)
                    .Update(tweet);
        }

        public async Task<bool> IsUserAuthorized(int tweetId)
        {
            var response = await _client.From<Tweet>()
                .Filter("TweetId", Constants.Operator.Equals, tweetId)
                .Filter("UserId", Constants.Operator.Equals, _userInfo.Id)
                .Get();

            if (response.Models.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
