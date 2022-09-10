using System.Runtime.CompilerServices;
using Common.Extension;
using Common.Middlewares;
using Common.Models;
using Library.Twitter.Extension;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Supabase;

namespace Twitter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddDistributedMemoryCache();

            builder.Services.BindCommon();
            builder.Services.BindTwitterLibrary();

            Client.Initialize("https://dssxaentuenuzoyuztgl.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRzc3hhZW50dWVudXpveXV6dGdsIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NjI4MzQ4NzksImV4cCI6MTk3ODQxMDg3OX0.h91D9DeUKokd7-HS4kER0RkSMmGZ3r1rH-uK36sxfRo");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<RequestPreprocessor>();

            app.Run();
        }

        private async static Task RequestPreprocessing(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path.StartsWithSegments("/api/Authentication"))
            {
                await next();
                return;
            }

            var user = context.Session.Get<User>("userInfo");
            if (user != null)
            {
                IUser? userInfo = context.RequestServices.GetService<IUser>();
                userInfo = user;

                await next();
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