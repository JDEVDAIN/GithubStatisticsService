﻿using GithubStatisticsCore.Models;
using GithubStatisticsCore.Services.GithubApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using GithubStatisticsCore.Services;
using GithubStatisticsCore.Services.DataService;
using Hangfire;
using Hangfire.MySql.Core;

namespace GithubStatisticsCore
{
    public class Startup
    {
        private readonly IBackgroundService _backgroundService;

        public Startup(IConfiguration configuration, IBackgroundService backgroundService)
        {
            Configuration = configuration;
            _backgroundService =
                backgroundService; //dependency injection of backgroundService, for Hangfire in configuration 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBackgroundService, BackgroundService>(); //TODO needed?
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); //todo??
            //Dependency injection
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
            //Database
            services.AddDbContext<GithubDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("GithubDbContext")));

            //Hangfire configuration
            services.AddHangfire(configuration => configuration
                .UseStorage(new MySqlStorage(Configuration.GetConnectionString("Hangfire"),
                    new MySqlStorageOptions() {TablePrefix = "Hangfire"})));

            services.AddHangfireServer();
            //Hangfire usage
            //Dependency injection
         

           // services.AddTransient<IBackgroundService, BackgroundService>();
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

            //Hangfire
            app.UseHangfireDashboard();
            //Hangfire Services

            RecurringJob.AddOrUpdate("AddOrUpdateGithubProjects", () => _backgroundService.RunDemoTask(),Cron.Daily);
        }
    }
}