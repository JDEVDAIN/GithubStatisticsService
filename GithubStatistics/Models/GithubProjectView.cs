using System;

namespace WebApplication1.Models
{
    public class GithubProjectView
    {
        public int count { get; set; }

        public int uniques { get; set; }

        public View[] Views { get; set; }
    }

    public class View //inner class is this ok? TODO
    {
        public DateTime timestamp { get; set; }
        public int count { get; set; }
        public int uniques { get; set; }
    }


}


