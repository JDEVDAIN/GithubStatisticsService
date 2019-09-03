using GithubStatisticsCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GithubStatisticsCore.Services.DataService
{
    public interface IGithubDataService
    {
        void SaveGithubProject(GithubProject githubProject);

        void SaveGithubProjects(List<GithubProject> githubProjects);

        void SaveGithubProjectView(GithubProjectView githubProjectView);

        void UpdateGithubProjects(List<GithubProject> githubProjects);

        void SaveGithubProjectViews(List<GithubProjectView> githubProjectViews);

        void SaveOrUpdateGithubProjectView(GithubProjectView githubProjectView);

        List<string> GetGithubProjectViewsNames();

        List<string> GetGithubProjectNames();
        List<GithubProject> GetGithubProjects();

        void SaveViews(List<GithubProjectView> githubProjectViews);

        void RemoveDuplicatesInViews();

        void RemoveDuplicatesInViewsAndUpdateTotal();
    }

    public class GithubDataService : IGithubDataService
    {
        private readonly GithubDbContext
            _context; //cant use SaveChangesAsync with Pomelo MYSQL 2.2.0. Or it will not save it.

        public GithubDataService(GithubDbContext context) //dependency injection
        {
            _context = context;
        }


        public void SaveGithubProject(GithubProject githubProject)
        {//TODO check if already existing
            _context.GithubProjects.Add(githubProject);
            _context.SaveChanges();
        }

        public void SaveGithubProjects(List<GithubProject> githubProjects)
        {
            List<string> projectNames = GetGithubProjectNames();

            foreach (GithubProject githubProject in githubProjects)
            {
                if (!projectNames.Contains(githubProject.Name))
                {
                    _context.GithubProjects.Add(githubProject); //TODO check out AddRange }
                }
                else
                {
                    //TODO logging
                }
            }

            //_context.SaveChangesAsync();
            _context.SaveChanges();
        }

        public void UpdateGithubProjects(List<GithubProject> githubProjects) //will not create projects
        {
            foreach (GithubProject githubProject in githubProjects)
            {
                _context.GithubProjects.Update(githubProject); //TODO check out AddRange
            }

            //_context.SaveChangesAsync();
            _context.SaveChanges();
        }


        public void SaveGithubProjectView(GithubProjectView githubProjectView)
        {
            List<string> githubProjectViewsNames = GetGithubProjectViewsNames();
            if (!githubProjectViewsNames.Contains(githubProjectView.Name))
            {
                _context.GithubProjectViews.Add(githubProjectView);
                _context.SaveChanges();
            }
            else
            {
                //log already exists TODO
            }

            //todo make sure it doesnt already exist else exception, maybe make catch exception or check first
        }

        public void SaveGithubProjectViews(List<GithubProjectView> githubProjectViews)
        {
            //also saves githubprojects?
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

            _context.SaveChanges();
        }

        public void SaveOrUpdateGithubProjectView(GithubProjectView githubProjectView)
        {
            //_context.GithubProjectViews.AddOrUpdate(githubProjectView);
            _context.GithubProjectViews.Update(githubProjectView); //TODO does it generate it?
            _context.SaveChanges();
        }

        public List<string> GetGithubProjectViewsNames()
        {
            return _context.GithubProjectViews.Select(p => p.Name).ToList();
        }
        public List<GithubProject> GetGithubProjects()
        {
            return _context.GithubProjects.ToList();
        }


        public List<string> GetGithubProjectNames()
        {
            return _context.GithubProjects.Select(p => p.Name).ToList();
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

            _context.SaveChanges();
        }

        public void RemoveDuplicatesInViews() //TODO improve
        {
            //get all views
            //if views have same GithubProjectView_Name (Query with Name?)
            //check for timestamp if its the same remove
            //if timestamp is not the same add the counts and uniques together and write to total in Githubprojectview
            List<GithubProjectView> githubProjectViews = _context.GithubProjectViews.Include(d => d.Views).ToList();
            foreach (var githubProjectView in githubProjectViews)
            {
                List<View> uniqueViewList = new List<View>();
                bool first = true;

                foreach (View databaseView in new List<View>(githubProjectView.Views)
                ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                {
                    if (first)
                    {
                        uniqueViewList.Add(databaseView);
                        first = false;
                    }
                    else
                    {
                        bool isDuplicate = false;
                        foreach (View uniqueView in new List<View>(uniqueViewList)
                        ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                        {
                            if (uniqueView.Timestamp.CompareTo(databaseView.Timestamp) == 0) //is a duplicate
                            {
                                isDuplicate = true;
                                _context.Entry(databaseView).State = EntityState.Deleted;

                            }
                        }
                        if (isDuplicate == false) { uniqueViewList.Add(databaseView); }
                    }
                }
            }

            _context.SaveChanges();
        }

        public void RemoveDuplicatesInViewsAndUpdateTotal() //TODO improve 
        {
            //get all views
            //if views have same GithubProjectView_Name (Query with Name?)
            //check for timestamp if its the same remove
            //if timestamp is not the same add the counts and uniques together and write to total in Githubprojectview
            List<GithubProjectView> githubProjectViews = _context.GithubProjectViews.Include(d => d.Views).ToList();
            System.Diagnostics.Debug.WriteLine("Test");
            //remove duplicates
            foreach (var githubProjectView in githubProjectViews)
            {
                List<View> uniqueViewList = new List<View>();
                bool first = true;

                foreach (View databaseView in new List<View>(githubProjectView.Views)
                ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                {
                    if (first)
                    {
                        uniqueViewList.Add(databaseView);
                        first = false;
                    }
                    else
                    {
                        bool isDuplicate = false;
                        foreach (View uniqueView in new List<View>(uniqueViewList)
                        ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                        {
                            if (uniqueView.Timestamp.CompareTo(databaseView.Timestamp) == 0) //is a duplicate
                            {
                                isDuplicate = true;
                                _context.Entry(databaseView).State = EntityState.Deleted;
                               
                            }
                        }
                        if (isDuplicate == false) { uniqueViewList.Add(databaseView);}
                    }

                    System.Diagnostics.Debug.WriteLine(uniqueViewList.ToString());
                    System.Diagnostics.Debug.WriteLine("\n");
                }

                //calculate total
                int totalViews = 0;
                int totalUniques = 0;
                foreach (View view in uniqueViewList)
                {
                    totalViews += view.Count;
                    totalUniques += view.Uniques;
                }

                githubProjectView.TotalCount = totalViews;
                githubProjectView.TotalUniques = totalUniques;
                //_context.Entry(githubProjectView).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }
    }
}