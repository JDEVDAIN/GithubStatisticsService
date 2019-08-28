using GithubStatisticsCore.Models;
using GithubStatisticsCore.Services.GithubApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GithubStatisticsCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);//todo??
            services.AddSingleton<IGithubApiRepoProcessor, GithubApiRepoProcessor>();
            IApiClientHelper apiClientHelper = new GithubApiClientHelper();
            apiClientHelper.InitializeClient();//TODO is this right? https://stackoverflow.com/questions/39005861/asp-net-core-initialize-singleton-after-configuring-di
            services.AddSingleton<IApiClientHelper>(apiClientHelper);

            services.AddDbContext<GithubDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("GithubDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
             //TODO replace with service start
//            GithubApiClientHelper
//                .InitializeClient(); // TOdo replace with factory https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#consumption-patterns

        }
    }
}