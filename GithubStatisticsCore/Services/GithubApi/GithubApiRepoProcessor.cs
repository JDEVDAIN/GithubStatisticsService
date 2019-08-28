using GithubStatisticsCore.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

        public GithubApiRepoProcessor(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
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
                githubProjects = await response.Content.ReadAsAsync<List<GithubProject>>();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
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
                    await GithubApiClientHelper.GithubClient.GetAsync(
                        $"https://api.github.com/repos/jdevdain/{githubProjects[i].Name}/traffic/views");
                System.Diagnostics.Debug.WriteLine(githubProjects[i].Name);

                if (response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine(response.Content);
                    //                    githubProjectViews.Add(githubProjects[i].Name,
                    //                        await response.Content.ReadAsAsync<GithubProjectView>());
                    //                    GithubProjectView githubProjectView = new GithubProjectView()
                    //                    {
                    //                        Name=githubProjects[i].Name
                    //
                    //                    };
                    GithubProjectView githubProjectView = await response.Content.ReadAsAsync<GithubProjectView>();
                    githubProjectView.Name = githubProjects[i].Name;
                    githubProjectViews.Add(githubProjectView);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
                    //Console.WriteLine(response.ReasonPhrase);
                }
            }

            return githubProjectViews;
        }
    }


}