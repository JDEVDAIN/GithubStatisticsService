using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using GithubStatistics.Models;
using WebApplication1.Models;

namespace GithubStatistics.Services.DataService
{

    public class GithubDataService
    {
        private readonly GithubDbContext _context = new GithubDbContext();

        public void SaveGithubProject(GithubProject githubProject)
        {
            _context.GithubProjects.Add(githubProject);
            _context.SaveChangesAsync();
        }

        public void SaveGithubProjects(List<GithubProject> githubProjects)
        {
            foreach (GithubProject githubProject in githubProjects) 
            {
                _context.GithubProjects.Add(githubProject); //TODO check out AddRange
            }

            _context.SaveChangesAsync();
        }

        public void SaveGithubProjectView(GithubProjectView githubProjectView)
        {
            List<string> githubProjectViewsNames = GetGithubProjectViewsNames();
            if (!githubProjectViewsNames.Contains(githubProjectView.Name))
            {
                _context.GithubProjectViews.Add(githubProjectView);
                _context.SaveChangesAsync();
            }
            else
            {
                //log already exists TODO
            }
            //todo make sure it doesnt already exist else exception, maybe make catch exception or check first

        }
        
        public void SaveGithubProjectViews(List<GithubProjectView> githubProjectViews)
        {
            List<string> githubProjectViewsNames = GetGithubProjectViewsNames();
            foreach (GithubProjectView githubProjectView in githubProjectViews)
            {
                if (!githubProjectViewsNames.Contains(githubProjectView.Name))
                {
                    _context.GithubProjectViews.Add(githubProjectView);
                }
                else
                {
                    //TODO log
                }
            }
            _context.SaveChangesAsync();

        }

        public void SaveOrUpdateGithubProjectView(GithubProjectView githubProjectView)
        {
            _context.GithubProjectViews.AddOrUpdate(githubProjectView);
            _context.SaveChangesAsync();
        }

        public List<string> GetGithubProjectViewsNames()
        {
            List<string> githubProjectViewsNameList;
            githubProjectViewsNameList = _context.GithubProjectViews.Select(p=>p.Name).ToList(); //Linq expression to get all names 


            return githubProjectViewsNameList;
        }

        public void SaveView(GithubProjectView githubProjectView)
        {
            _context.Entry(View).

        }



    }
}