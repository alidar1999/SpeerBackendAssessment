using System.Collections;
using System.Text;
using Common.Business.Authentication;
using Common.Data;
using Common.Data.User;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Common.Extension
{
    public static class CommonExtension
    {
        public static void BindCommon(this IServiceCollection services)
        {
            services.AddScoped<IAuth, UserAuth>()
                    .AddScoped<IUserData, UserSupabaseHelper>()
                    .AddSingleton<IClient, DataClient>()
                    .AddScoped<IUser>(provider =>
                    {
                        var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;

                        var userSession = httpContext.Session.Get<User>("userInfo");
                        
                        if (!string.IsNullOrEmpty(userSession?.Username))
                        {
                            return provider.GetRequiredService<IAuth>().GetUserByUsername(userSession.Username).Result;
                        }

                        return null;
                    }
                );
        }
    }
}
