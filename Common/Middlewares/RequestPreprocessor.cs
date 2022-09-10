using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extension;
using Common.Models;
using Microsoft.AspNetCore.Http;

namespace Common.Middlewares
{
    public class RequestPreprocessor
    {
        private readonly RequestDelegate _next;

        public RequestPreprocessor(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/Authentication"))
            {
                await _next(context);
                return;
            }

            var userSession = context.Session.Get<User>("userInfo");

            if (!string.IsNullOrEmpty(userSession?.Username))
            {
                await _next(context);
                return;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
    }
}
