﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GithubStatisticsCore.Models;
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
        public async Task<ActionResult<List<View>>> GetViews()
        {
            return await _githubDataService.GetViews();
        }

        // GET: api/GithubProjectViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GithubProjectView>> GetGithubProjectView(string id)
        {
            var githubProjectView = await _context.GithubProjectViews.FindAsync(id);

            if (githubProjectView == null)
            {
                return NotFound();
            }

            return githubProjectView;
        }
    }
}
