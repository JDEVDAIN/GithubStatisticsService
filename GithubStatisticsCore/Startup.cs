using GithubStatisticsCore.Models;
using GithubStatisticsCore.Services.GithubApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using GithubStatisticsCore.Services.DataService;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); //todo??
            services.AddScoped<IGithubApiRepoProcessor, GithubApiRepoProcessor>();
            services.AddScoped<IGithubDataService, GithubDataService>();
            services.AddHttpClient("Github", client =>
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("User-agent", "github_repo_client");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(
                                System.Text.Encoding.ASCII.GetBytes(
                                    $"Bot-dain:38907eec13052be99e1b06435ca76d6f3d084360"))); //TODO remove oauth from code?
                }
            );

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