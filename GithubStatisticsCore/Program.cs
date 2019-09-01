using GithubStatisticsCore.Models;
using GithubStatisticsCore.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GithubStatisticsCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(serviceCollection =>
                    serviceCollection
                        .AddScoped<IBackgroundService, BackgroundService
                        >()) //https://nodogmablog.bryanhogan.net/2018/05/using-dependency-injection-with-startup-in-asp-net-core/
                .UseStartup<Startup>();
    }
}