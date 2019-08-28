using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace GithubStatistics.Models
{
    public class GithubProjectView //TODO split api model and database model. what are dtos exactly
    {
        [Key] public string Name { get; set; }
        //        [JsonProperty("count")]
        //        public int Count { get; set; } //useless
        //        [JsonProperty("uniques")]
        //        public int Uniques { get; set; } //useless 

        public ICollection<View> Views { get; set; }

        public int TotalCount { get; set; }

        public int TotalUniques { get; set; }

        [ForeignKey("Name")] public GithubProject GithubProject { get; set; } //is that good mapping? TODO
    }

    public class View
    {
        [Key] public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        [JsonProperty("count")] public int Count { get; set; }
        [JsonProperty("uniques")] public int Uniques { get; set; }

        // public string ViewName { get; set; } //needed? todo

        public virtual GithubProjectView GithubProjectView { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(Timestamp)}: {Timestamp}, {nameof(Count)}: {Count}, {nameof(Uniques)}: {Uniques}, {nameof(GithubProjectView)}: {GithubProjectView}";
        }
    }
}