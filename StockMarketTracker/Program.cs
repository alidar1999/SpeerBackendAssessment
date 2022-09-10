using Common.Extension;
using Common.Middlewares;
using Library.StockMarketTracker.Extension;
using Supabase;

namespace StockMarketTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddDistributedMemoryCache();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.BindCommon();

            builder.Services.BindStockMarketLibrary();

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
    }
}