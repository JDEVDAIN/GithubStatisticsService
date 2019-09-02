//using GithubStatisticsCore.Models;
//using GithubStatisticsCore.Services.DataService;
//using GithubStatisticsCore.Services.GithubApi;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//
//namespace GithubStatisticsCore.Controllers
//{
//    public class GithubStatisticsController : Controller
//    {
//        private readonly IGithubApiRepoProcessor _githubApiRepoProcessor;//TODO make request automated //TODO make interface
//
//        private readonly IGithubDataService _githubDataService;
//
//        public GithubStatisticsController(IGithubApiRepoProcessor githubApiRepoProcessor, IGithubDataService githubDataService)
//        {
//            this._githubApiRepoProcessor = githubApiRepoProcessor;
//            this._githubDataService = githubDataService;
//        }
//
//
//
//        //        public ValuesController(GithubApiRepoProcessor githubApiRepoProcessor)
//        //        {
//        //            this._githubApiRepoProcessor = githubApiRepoProcessor;
//        //        }
//        //private readonly GithubDbContext _context = new GithubDbContext();
//        // GET api/values
//        [HttpGet]
//        [Route("Projects")]
//        public async Task<List<GithubProject>> Get() //gets info to projects always needed
//        {
//            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
//            _githubDataService.SaveGithubProjects(githubProjects);
//
//            return githubProjects;
//        }
//
//        [HttpGet]
//        [Route("updateProjects")]
//        public async Task<List<GithubProject>> UpdateProjectsGet() //gets info to projects always needed
//        {
//            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
//            _githubDataService.UpdateGithubProjects(githubProjects);
//
//            return githubProjects;
//        }
//
//        // GET api/values/5
//        [HttpGet]
//        [Route("showViews")] //if called and database is empty it will create projects and views only thing missing is the totalof views
//        public async Task<List<GithubProjectView>>
//            Get(int id) //updated whole GithubProjectViews Table, only needed if there is a new project
//        {
//            List<GithubProject> githubProjects = await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain");
//            List<GithubProjectView> githubProjectViews =
//                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects); //replace with database access TODO
//
//            _githubDataService.SaveGithubProjectViews(githubProjectViews);
//
//            return githubProjectViews;
//        }
//
//        [HttpGet]
//        [Route("ViewNames")]
//        public List<string> GetGithubProjectViewsNames()
//        {
//            return _githubDataService.GetGithubProjectViewsNames();
//        }
//
//        [HttpGet]
//        [Route("Views")]
//        public async Task<List<View>> GetGithubProjectViews()
//        {
//            List<GithubProject>
//                githubProjects =
//                    await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain"); //Replace with database access TODO
//            List<GithubProjectView> githubProjectViews =
//                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects); //Replace with database access TODO
//            List<View> viewList = new List<View>();
//            foreach (GithubProjectView githubProjectView in githubProjectViews)
//            {
//                viewList = viewList.Concat(githubProjectView.Views.ToList()).ToList();
//            }
//
//            //_githubDataService.SaveViews(githubProjectViews);
//            return viewList;
//        }
//
//        [HttpGet]
//        [Route("saveViews")]
//        public async Task<List<GithubProjectView>> SaveGithubProjectViews()
//        {
//            List<GithubProject>
//                githubProjects =
//                    await _githubApiRepoProcessor.GetGithubRepoInfo("jdevdain"); //Replace with database access TODO
//            List<GithubProjectView> githubProjectViews =
//                await _githubApiRepoProcessor.GetGithubRepoViews(githubProjects); //Replace with database access TODO
//            _githubDataService.SaveViews(githubProjectViews);
//
//            //_githubDataService.SaveViews(githubProjectViews);
//            return githubProjectViews;
//        }
//
//        [HttpGet]
//        [Route("removeDuplicates")]
//        public async Task<OkResult> RemoveDuplicatesInViews()
//        {
//            _githubDataService.RemoveDuplicatesInViews();
//
//            return Ok();
//        }
//
//        [HttpGet]
//        [Route("updateDuplicates")]
//        public async Task<OkResult> RemoveDuplicatesAndUpdateInViews()
//        {
//            _githubDataService.RemoveDuplicatesInViewsAndUpdateTotal();
//
//            return Ok();
//        }
//
//    }
//}
