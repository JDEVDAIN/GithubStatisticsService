using System.Collections.Generic;
using System.Threading.Tasks;
using GithubStatisticsCore.Models;
using GithubStatisticsCore.Services.DataService;
using GithubStatisticsCore.Services.GithubApi;

namespace GithubStatisticsCore.Services
{
    public interface IGithubService
    {
        Task AddNewProjects();
        Task UpdateProjects();
        Task AddNewProjectsAndUpdateExisting();
        Task SaveViews();
        void RemoveDuplicatesAndCalculateTotalViews();
    }

    public class GithubService : IGithubService
    {
        private readonly IGithubApiRepoProcessor _githubApiRepoProcessor;
        private readonly IGithubDataService _githubDataService;

        public GithubService(IGithubDataService githubDataService, IGithubApiRepoProcessor githubApiRepoProcessor)
        {
            _githubDataService = githubDataService;
            _githubApiRepoProcessor = githubApiRepoProcessor;
        }

        public async Task AddNewProjects() //TODO async vs async task
        {
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            _githubDataService.SaveGithubProjects(githubProjects);
        }

        public async Task UpdateProjects()
        {
            //TODO change to Database access
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            _githubDataService.UpdateGithubProjects(githubProjects);
        }

        public async Task AddNewProjectsAndUpdateExisting()
        {
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            _githubDataService.SaveGithubProjects(githubProjects);
            _githubDataService.UpdateGithubProjects(githubProjects);
        }

        public async Task SaveViews()
        {
            List<GithubProject> githubProjects = _githubDataService.GetGithubProjects();
            List<GithubProjectView> githubProjectViews =
                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects);
            _githubDataService.SaveViews(githubProjectViews);
        }

        public void RemoveDuplicatesAndCalculateTotalViews()
        {
            _githubDataService.RemoveDuplicatesInViewsAndUpdateTotal();
        }
    }
}