using System.Data;
using Library.Twitter.Models;

namespace Library.Twitter.Data
{
    public interface ITweetData
    {
        public Task Create(Tweet tweet);
        public Task Update(Tweet tweet);
        public Task<DataTable> Read(int tweetId);
        public Task Delete(int tweetId);
        public Task<bool> IsUserAuthorized(int tweetId);
    }
}
