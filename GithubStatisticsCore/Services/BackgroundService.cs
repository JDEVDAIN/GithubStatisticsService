using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GithubStatisticsCore.Models;
using GithubStatisticsCore.Services.DataService;
using GithubStatisticsCore.Services.GithubApi;

namespace GithubStatisticsCore.Services
{
    public interface IBackgroundService
    {
        void AddNewProjects();
        void UpdateProjects();
        void SaveViews();
        void RemoveAndCalculateTotalViews();
    }

    public class BackgroundService : IBackgroundService
    {
        private readonly IGithubDataService _githubDataService;
        private readonly IGithubApiRepoProcessor _githubApiRepoProcessor;

        public BackgroundService(IGithubDataService githubDataService, IGithubApiRepoProcessor githubApiRepoProcessor)
        {
            _githubDataService = githubDataService;
            _githubApiRepoProcessor = githubApiRepoProcessor;
        }


        public async void AddNewProjects() //TODO async vs async task
        {
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            _githubDataService.SaveGithubProjects(githubProjects);
        }

        public async void UpdateProjects()
        {
            //TODO change to Database access
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            _githubDataService.UpdateGithubProjects(githubProjects);
        }

        public async void SaveViews()
        {
            List<GithubProjectView> githubProjectViews = await _githubApiRepoProcessor.GetGithubRepoViews("jdevdain");
            _githubDataService.SaveViews(githubProjectViews);
        }

        public void RemoveAndCalculateTotalViews()
        {
            _githubDataService.RemoveDuplicatesInViewsAndUpdateTotal();
        }
    }
}