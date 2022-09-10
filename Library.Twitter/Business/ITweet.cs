using Library.Twitter.Enums;
using Library.Twitter.Models;

namespace Library.Twitter.Business
{
    public interface ITweet
    {
        public Task<TweetActionResponse> Create(Tweet tweet);
        public Task<TweetActionResponse> Update(Tweet tweet);
        public Task<Tweet> Read(int tweetId);
        public Task<TweetActionResponse> Delete(int tweetId);
    }
}
