﻿using GithubStatisticsCore.Models;
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

        void SaveGithubProjectViews(List<GithubProjectView> githubProjectViews);

        void SaveOrUpdateGithubProjectView(GithubProjectView githubProjectView);

        List<string> GetGithubProjectViewsNames();
        void SaveViews(List<GithubProjectView> githubProjectViews);

        void RemoveDuplicatesInViews();

        void RemoveDuplicatesInViewsAndUpdateTotal();
    }

    public class GithubDataService : IGithubDataService
    {
        private readonly GithubDbContext _context;

        public GithubDataService(GithubDbContext context) //dependency injection
        {
            _context = context;
        }


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

            _context.SaveChangesAsync();
        }

        public void SaveOrUpdateGithubProjectView(GithubProjectView githubProjectView)
        {
            //_context.GithubProjectViews.AddOrUpdate(githubProjectView);
            _context.GithubProjectViews.Update(githubProjectView); //TODO does it generate it?
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
                List<View> uniqueViewList = new List<View>();
                bool first = true;

                foreach (View datebaseView in new List<View>(githubProjectView.Views)
                ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                {
                    if (first)
                    {
                        uniqueViewList.Add(datebaseView);
                        first = false;
                    }
                    else
                    {
                        foreach (View uniqueView in new List<View>(uniqueViewList)
                        ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                        {
                            if (uniqueView.Timestamp.CompareTo(datebaseView.Timestamp) != 0) //not a duplicate
                            {
                                uniqueViewList.Add(datebaseView);
                            }
                            else
                            {
                                _context.Entry(datebaseView).State = EntityState.Deleted;
                            }
                        }
                    }

                    System.Diagnostics.Debug.WriteLine(uniqueViewList.ToString());
                    System.Diagnostics.Debug.WriteLine("\n");
                }
            }

            _context.SaveChangesAsync();
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
                        foreach (View uniqueView in new List<View>(uniqueViewList)
                        ) //workaround needed else it will reference the original databaselist and if something gets deleted it will remove it from the list and make it impossible to run
                        {
                            if (uniqueView.Timestamp.CompareTo(databaseView.Timestamp) != 0) //not a duplicate
                            {
                                uniqueViewList.Add(databaseView);
                            }
                            else
                            {
                                _context.Entry(databaseView).State = EntityState.Deleted;
                            }
                        }
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

            _context.SaveChangesAsync();
        }
    }
}