using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using GithubStatistics.Services;
using WebApplication1.Models;

namespace GithubStatistics.Controllers
{

    public class ValuesController : ApiController
    {
        private readonly GithubRepoProcessor _githubRepoProcessor = new GithubRepoProcessor(); //TODO make request automated

        // GET api/values
        public async Task<List<GithubProject>> Get()
        {

            return await _githubRepoProcessor.GetGithubRepoInfo("jdevdain");


        }

        // GET api/values/5
        public async Task<Dictionary<string, GithubProjectView>> Get(int id)
        {

            List<GithubProject> githubProjects = await _githubRepoProcessor.GetGithubRepoInfo("jdevdain");

            return await _githubRepoProcessor.GetGithubRepoViews(githubProjects);

        }

        // POST api/values
        ///
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
