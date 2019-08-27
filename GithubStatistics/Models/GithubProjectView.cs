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
        public int Count { get; set; } //useless

        public int Uniques { get; set; } //useless 

        public ICollection<View> Views { get; set; }

        public int TotalCount { get; set; }

        public int TotalUniques { get; set; }

        [ForeignKey("Name")]
        public GithubProject GithubProject { get; set; } //is that good mapping? TODO

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Count)}: {Count}, {nameof(Uniques)}: {Uniques}, {nameof(Views)}: {Views.ToString()}, {nameof(GithubProject)}: {GithubProject}";
        }
    }

    public class View
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
        public int Uniques { get; set; }

        // public string ViewName { get; set; } //needed? todo

        public virtual GithubProjectView GithubProjectView { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Timestamp)}: {Timestamp}, {nameof(Count)}: {Count}, {nameof(Uniques)}: {Uniques}, {nameof(GithubProjectView)}: {GithubProjectView}";
        }
    }


}


