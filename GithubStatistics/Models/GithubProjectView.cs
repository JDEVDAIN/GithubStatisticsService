using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{

    public class GithubProjectView //TODO split api model and database model. what are dtos exactly
    {
        [Key]
        public string Name { get; set; }
        public int count { get; set; }

        public int uniques { get; set; }

        public ICollection<View> Views { get; set; }

        public int TotalCount { get; set; }

        public int TotalUniques { get; set; }

        [ForeignKey("Name")]
        public GithubProject GithubProject { get; set; } //is that good mapping? TODO

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(count)}: {count}, {nameof(uniques)}: {uniques}, {nameof(Views)}: {Views.ToString()}, {nameof(GithubProject)}: {GithubProject}";
        }
    }

    public class View 
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime timestamp { get; set; }
        public int count { get; set; }
        public int uniques { get; set; }
    }


}


