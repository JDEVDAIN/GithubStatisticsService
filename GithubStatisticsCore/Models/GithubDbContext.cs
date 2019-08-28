using Microsoft.EntityFrameworkCore;

namespace GithubStatisticsCore.Models
{
    

    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GithubDbContext : DbContext
    {
        // Your context has been configured to use a 'GithubDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'GithubStatistics.Models.GithubDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'GithubDbContext' 
        // connection string in the application configuration file.
        public GithubDbContext(DbContextOptions<GithubDbContext> options)
            : base(options)
        { }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<GithubProject> GithubProjects { get; set; }
        public virtual DbSet<GithubProjectView> GithubProjectViews { get; set; }
    }
}