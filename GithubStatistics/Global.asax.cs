using GithubStatistics.Services;
using System.Web.Http;

namespace GithubStatistics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GithubApiClientHelper
                .InitializeClient(); // TOdo replace with factory https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#consumption-patterns

        }
    }
}
