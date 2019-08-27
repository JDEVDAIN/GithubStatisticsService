using System;
using GithubStatistics.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
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
            githubProjectViewsNameList =
                _context.GithubProjectViews.Select(p => p.Name).ToList(); //Linq expression to get all names 


            return githubProjectViewsNameList;
        }

        public void SaveViews(List<GithubProjectView> githubProjectViews)
        {
            List<GithubProjectView> databaseGithubProjectViews =
                _context.GithubProjectViews.Include(d => d.Views).ToList();
            foreach (var databaseGithubProjectView in databaseGithubProjectViews)
            {
                int index = githubProjectViews.IndexOf(githubProjectViews.Find(x =>
                    x.Name.Equals(databaseGithubProjectView.Name)));
                foreach (View view in githubProjectViews[index].Views)
                {
                    databaseGithubProjectView.Views.Add(view);
                }
            }

            _context.SaveChangesAsync();
        }

        public void RemoveDuplicatesInViews() //TODO improve
        {
            //get all views
            //if views have same GithubProjectView_Name (Query with Name?)
            //check for timestamp if its the same remove
            //if timestamp is not the same add the counts and uniques together and write to total in Githubprojectview
            List<GithubProjectView> githubProjectViews = _context.GithubProjectViews.Include(d => d.Views).ToList();
            System.Diagnostics.Debug.WriteLine("Test");
            foreach (var githubProjectView in githubProjectViews)
            {
                List<View> viewList = new List<View>();
                bool first = true;
                var test = githubProjectView.Views;
                foreach (View view in new List<View>(githubProjectView.Views)
                ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                {
                    if (first)
                    {
                        viewList.Add(view);
                        first = false;
                    }
                    else
                    {
                        foreach (View view1 in new List<View>(viewList)
                        ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                        {
                            if (view1.Timestamp.CompareTo(view.Timestamp) != 0) //not a duplicate
                            {
                                viewList.Add(view);
                            }
                            else
                            {
                                _context.Entry(view).State = EntityState.Deleted;
                            }
                        }
                    }

                    System.Diagnostics.Debug.WriteLine(viewList.ToString());
                    System.Diagnostics.Debug.WriteLine("\n");
                }
            }

            _context.SaveChangesAsync();
        }
    }
}