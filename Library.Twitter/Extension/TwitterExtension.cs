using Library.Twitter.Business;
using Library.Twitter.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Twitter.Extension
{
    public static class TwitterExtension
    {
        public static void BindTwitterLibrary(this IServiceCollection services)
        {
            services.AddScoped<ITweetData, TweetSupabaseHelper>()
                    .AddScoped<ITweet, Business.Twitter>();
        }
    }
}
