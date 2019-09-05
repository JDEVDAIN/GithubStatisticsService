using GithubStatisticsCore.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GithubStatisticsCore.Services.GithubApi
{
    public interface IGithubApiRepoProcessor
    {
        Task<List<GithubProject>> GetGithubRepoInfo(string user);
        //TODO remove task or not? figure out async in c#
        Task<List<GithubProjectView>> GetGithubRepoViews(List<GithubProject> githubProjects);
    }
    public class GithubApiRepoProcessor : IGithubApiRepoProcessor
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger _logger;

        public GithubApiRepoProcessor(IHttpClientFactory clientFactory, ILogger<GithubApiRepoProcessor> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }


        public async Task<List<GithubProject>> GetGithubRepoInfo(string user = "jdevdain")
        {

            var client = _clientFactory.CreateClient("Github");
            //Request 1
            HttpResponseMessage response =
                await client.GetAsync($"https://api.github.com/users/{user}/repos");
            List<GithubProject> githubProjects = null;
            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug($"{response.StatusCode}: {response.RequestMessage} ");
                githubProjects = await response.Content.ReadAsAsync<List<GithubProject>>();
            }
            else
            {
                _logger.LogError($"{response.StatusCode}: {response.RequestMessage} ");
            }


            return githubProjects;
        }


        public async Task<List<GithubProjectView>> GetGithubRepoViews(List<GithubProject> githubProjects)
        {
            var client = _clientFactory.CreateClient("Github");

            //Dictionary<string, GithubProjectView> githubProjectViews = new Dictionary<string, GithubProjectView>();
            List<GithubProjectView> githubProjectViews = new List<GithubProjectView>();
            for (int i = 0; i < githubProjects.Count - 1; i++)
            {
                HttpResponseMessage response =
                    await client.GetAsync(
                        $"https://api.github.com/repos/jdevdain/{githubProjects[i].Name}/traffic/views");
                

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogDebug($"{response.StatusCode}: {response.RequestMessage} ");
                    GithubProjectView githubProjectView = await response.Content.ReadAsAsync<GithubProjectView>();
                    githubProjectView.Name = githubProjects[i].Name;
                    githubProjectViews.Add(githubProjectView);
                }
                else
                {
                    _logger.LogError($"{response.StatusCode}: {response.RequestMessage} ");
                }
            }

            return githubProjectViews;
        }
    }


}