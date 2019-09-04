using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GithubStatisticsCore.Models;

namespace GithubStatisticsCore.Controllers
{
    [Route("api/totalViews")]
    [ApiController]
    public class GithubProjectViewsController : ControllerBase
    {
        private readonly GithubDbContext _context;

        public GithubProjectViewsController(GithubDbContext context)
        {
            _context = context;
        }

        // GET: api/GithubProjectViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GithubProjectView>>> GetGithubProjectViews()
        {
            return await _context.GithubProjectViews.ToListAsync();
        }

        // GET: api/GithubProjectViews/5
        [HttpGet("{name}")]
        public async Task<ActionResult<GithubProjectView>> GetGithubProjectView(string name)
        {
            var githubProjectView = await _context.GithubProjectViews.FindAsync(name);

            if (githubProjectView == null)
            {
                return NotFound();
            }

            return githubProjectView;
        }
    }
}
