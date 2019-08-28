using GithubStatistics.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GithubStatistics.Services
{
    public class GithubApiRepoProcessor
    {
        public async Task<List<GithubProject>> GetGithubRepoInfo(string user = "jdevdain")
        {
            //Request 1


            HttpResponseMessage response =
                await GithubApiClientHelper.GithubClient.GetAsync($"https://api.github.com/users/{user}/repos");
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