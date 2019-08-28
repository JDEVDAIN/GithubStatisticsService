using GithubStatistics.Models;
using GithubStatistics.Services;
using GithubStatistics.Services.DataService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication1.Models;

namespace GithubStatistics.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly GithubApiRepoProcessor
            _githubApiRepoProcessor =
                new GithubApiRepoProcessor(); //TODO make request automated //TODO Dependency injection

        private readonly GithubDataService _githubDataService = new GithubDataService();

        //        public ValuesController(GithubApiRepoProcessor githubApiRepoProcessor)
        //        {
        //            this._githubApiRepoProcessor = githubApiRepoProcessor;
        //        }
        //private readonly GithubDbContext _context = new GithubDbContext();
        // GET api/values
        public async Task<List<GithubProject>> Get() //gets info to projects always needed
        {
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            _githubDataService.SaveGithubProjects(githubProjects);

            return githubProjects;
        }

        // GET api/values/5
        public async Task<List<GithubProjectView>>
            Get(int id) //updated whole GithubProjectViews Table, only needed if there is a new project
        {
            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
            List<GithubProjectView> githubProjectViews =
                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects); //replace with database access TODO

            _githubDataService.SaveGithubProjectViews(githubProjectViews);

            return githubProjectViews;
        }

        [HttpGet]
        [Route("ViewNames")]
        public List<string> GetGithubProjectViewsNames()
        {
            return _githubDataService.GetGithubProjectViewsNames();
        }

        [HttpGet]
        [Route("Views")]
        public async Task<List<View>> GetGithubProjectViews()
        {
            List<GithubProject>
                githubProjects =
                    await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain"); //Replace with database access TODO
            List<GithubProjectView> githubProjectViews =
                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects); //Replace with database access TODO
            List<View> viewList = new List<View>();
            foreach (GithubProjectView githubProjectView in githubProjectViews)
            {
                viewList = viewList.Concat(githubProjectView.Views.ToList()).ToList();
            }

            //_githubDataService.SaveViews(githubProjectViews);
            return viewList;
        }

        [HttpGet]
        [Route("saveViews")]
        public async Task<List<GithubProjectView>> SaveGithubProjectViews()
        {
            List<GithubProject>
                githubProjects =
                    await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain"); //Replace with database access TODO
            List<GithubProjectView> githubProjectViews =
                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects); //Replace with database access TODO
            _githubDataService.SaveViews(githubProjectViews);

            //_githubDataService.SaveViews(githubProjectViews);
            return githubProjectViews;
        }

        [HttpGet]
        [Route("removeDuplicates")]
        public async Task<OkResult> RemoveDuplicatesInViews()
        {
            _githubDataService.RemoveDuplicatesInViews();

            return Ok();
        }

        [HttpGet]
        [Route("updateDuplicates")]
        public async Task<OkResult> RemoveDuplicatesAndUpdateInViews()
        {
            _githubDataService.RemoveDuplicatesInViewsAndUpdateTotal();

            return Ok();
        }

    }
}