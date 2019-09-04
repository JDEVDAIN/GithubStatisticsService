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
    [Route("api/Projects")]
    [ApiController]
    public class GithubProjectsController : ControllerBase
    {
        private readonly GithubDbContext _context;

        public GithubProjectsController(GithubDbContext context)
        {
            _context = context;
        }

        // GET: api/GithubProjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GithubProject>>> GetGithubProjects()
        {
            return await _context.GithubProjects.ToListAsync();
        }

        // GET: api/GithubProjects/nameOfProject
        [HttpGet("{name}")]
        public async Task<ActionResult<GithubProject>> GetGithubProject(string name)
        {
            var githubProject = await _context.GithubProjects.FindAsync(name);

            if (githubProject == null)
            {
                return NotFound();
            }

            return githubProject;
        }
    }
}
