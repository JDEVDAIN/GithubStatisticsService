using Newtonsoft.Json;
using System;

namespace WebApplication1.Models
{
    public class GithubProject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public bool Fork { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
        //public string Forks { get; set; } //what is Forks compared to ForkCount

        public string Description { get; set; }

        [JsonProperty("stargazers_count")] public string StargazersCount { get; set; }

        public string Language { get; set; }
        [JsonProperty("forks_count")] public int ForkCount { get; set; }


        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Url)}: {Url}, {nameof(Fork)}: {Fork}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(Description)}: {Description}, {nameof(StargazersCount)}: {StargazersCount}, {nameof(Language)}: {Language}, {nameof(ForkCount)}: {ForkCount}";
        }
    }
}