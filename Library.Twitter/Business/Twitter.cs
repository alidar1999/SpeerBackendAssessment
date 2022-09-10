using Common.Models;
using Library.Twitter.Data;
using Library.Twitter.Enums;
using Library.Twitter.Models;

namespace Library.Twitter.Business
{
    public class Twitter : ITweet
    {
        private readonly ITweetData _tweetData;
        private readonly IUser _userInfo;
        public Twitter(ITweetData tweetData, IUser userInfo)
        {
            _tweetData = tweetData;
            _userInfo = userInfo;
        }

        public async Task<TweetActionResponse> Create(Tweet tweet)
        {
            tweet.UserId = _userInfo.Id;
            tweet.TweetCreateDateTimeUTC = DateTime.UtcNow;
            tweet.TweetModifiedDateTimeUTC = DateTime.UtcNow;

            await _tweetData.Create(tweet);
            return TweetActionResponse.Success;
        }

        public async Task<TweetActionResponse> Delete(int tweetId)
        {
            var result = await IsUserAuthorized(tweetId);
            if (result)
            {
                await _tweetData.Delete(tweetId);
                return TweetActionResponse.Success;
            }
            else
            {
                return TweetActionResponse.UnAuthorized;
            }
        }

        public async Task<Tweet?> Read(int tweetId)
        {
            var datatable = await _tweetData.Read(tweetId);
            if (datatable.Rows.Count > 0)
            {
                return new Tweet
                {
                    Subject = Convert.ToString(datatable.Rows[0]["Subject"]),
                    Description = Convert.ToString(datatable.Rows[0]["Description"]),
                    Id = Convert.ToInt32(datatable.Rows[0]["TweetId"]),
                    UserId = _userInfo.Id,
                    TweetCreateDateTimeUTC = Convert.ToDateTime(datatable.Rows[0]["TweetCreateDateTimeUTC"]),
                    TweetModifiedDateTimeUTC = Convert.ToDateTime(datatable.Rows[0]["TweetModifiedDateTimeUTC"])
                };
            }
            return null;
        }

        public async Task<TweetActionResponse> Update(Tweet tweet)
        {
            var result = await IsUserAuthorized(tweet.Id);
            if (result)
            {
                var originalTweet = await Read(tweet.Id);

                tweet.TweetCreateDateTimeUTC = originalTweet?.TweetCreateDateTimeUTC;
                tweet.TweetModifiedDateTimeUTC = DateTime.UtcNow;
                tweet.UserId = _userInfo.Id;

                await _tweetData.Update(tweet);
                return TweetActionResponse.Success;
            }
            else
            {
                return TweetActionResponse.UnAuthorized;
            }
        }

        private async Task<bool> IsUserAuthorized(int tweetId)
        {
            return await _tweetData.IsUserAuthorized(tweetId);
        }
    }
}
