namespace GithubStatistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class github1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Views", "ViewName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Views", "ViewName", c => c.String(unicode: false));
        }
    }
}
