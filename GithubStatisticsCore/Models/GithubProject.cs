using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GithubStatisticsCore.Models
{
    public class GithubProject
    {
        // public int Id { get; set; }

        [Key]
        [Required]
        public string Name { get; set; }

        [JsonProperty("html_url")]
        public string Url { get; set; }

        public bool Fork { get; set; } //is it a forked Project or own Project

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
        //public string Forks { get; set; } //what is Forks compared to ForkCount

        public string Description { get; set; }

        [JsonProperty("stargazers_count")] public string StargazersCount { get; set; }

        public string Language { get; set; }
        [JsonProperty("forks_count")] public int ForkCount { get; set; }



    }
}