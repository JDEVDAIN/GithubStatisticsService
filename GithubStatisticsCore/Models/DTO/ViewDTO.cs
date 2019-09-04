using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubStatisticsCore.Models.DTO
{
    public class ViewDto
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
        public int Uniques { get; set; }
        public string GithubProjectViewName { get; set; }
    }
}