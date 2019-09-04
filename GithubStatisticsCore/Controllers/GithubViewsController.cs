using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GithubStatisticsCore.Models;
using GithubStatisticsCore.Models.DTO;
using GithubStatisticsCore.Services.DataService;

namespace GithubStatisticsCore.Controllers
{
    [Route("api/Views")]
    [ApiController]
    public class GithubViewsController : ControllerBase
    {
        private readonly GithubDbContext _context;
        private readonly IGithubDataService _githubDataService;


        public GithubViewsController(GithubDbContext context, IGithubDataService githubDataService)
        {
            _context = context;
            _githubDataService = githubDataService;
        }

        // GET: api/GithubProjectViews
        [HttpGet]
        public async Task<ActionResult<List<ViewDto>>> GetViews()
        {
            return await _githubDataService.GetViews();
        }

        // GET: api/GithubProjectViews/nameOfProject
        [HttpGet("{name}")]
        public async Task<ActionResult<List<ViewDto>>> GetGithubView(string name)
        {
            var githubProjectView = await _githubDataService.GetView(name);

            if (githubProjectView == null || githubProjectView.Count == 0)
            {
                return NotFound();
            }

            return githubProjectView;
        }
    }
}
