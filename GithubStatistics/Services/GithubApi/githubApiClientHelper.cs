using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GithubStatistics.Services
{
    //    public interface IApiClientHelper
    //    {
    //        void InitializeClient();
    //    }

    public class GithubApiClientHelper//: IApiClientHelper //TODO make singleton or use factory see Global.asax.cs
    {
        public static HttpClient GithubClient { get; set; }

        public static void InitializeClient()
        {
            GithubClient = new HttpClient();
            // githubClient.BaseAddress = new Uri("https://api.github.com/users/jdevdain/");

            GithubClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GithubClient.DefaultRequestHeaders.Add("User-agent", "github_repo_client");
            GithubClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes(
                            $"Bot-dain:38907eec13052be99e1b06435ca76d6f3d084360")));
            //githubClient.DefaultRequestHeaders.Add("Authorization", "Bearer", "Your Oauth token");
            //("Bot-dain", "38907eec13052be99e1b06435ca76d6f3d084360");
        }

    }
}