namespace GithubStatistics.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class github : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GithubProjects",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Url = c.String(unicode: false),
                    Fork = c.Boolean(nullable: false),
                    CreatedAt = c.DateTime(nullable: false, precision: 0),
                    UpdatedAt = c.DateTime(nullable: false, precision: 0),
                    Description = c.String(unicode: false),
                    StargazersCount = c.String(unicode: false),
                    Language = c.String(unicode: false),
                    ForkCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.GithubProjectViews",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    count = c.Int(nullable: false),
                    uniques = c.Int(nullable: false),
                    TotalCount = c.Int(nullable: false),
                    TotalUniques = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.GithubProjects", t => t.Name)
                .Index(t => t.Name);

            CreateTable(
                "dbo.Views",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    timestamp = c.DateTime(nullable: false, precision: 0),
                    count = c.Int(nullable: false),
                    uniques = c.Int(nullable: false),
                    ViewName = c.String(unicode: false),
                    GithubProjectView_Name = c.String(maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GithubProjectViews", t => t.GithubProjectView_Name)
                .Index(t => t.GithubProjectView_Name);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Views", "GithubProjectView_Name", "dbo.GithubProjectViews");
            DropForeignKey("dbo.GithubProjectViews", "Name", "dbo.GithubProjects");
            DropIndex("dbo.Views", new[] { "GithubProjectView_Name" });
            DropIndex("dbo.GithubProjectViews", new[] { "Name" });
            DropTable("dbo.Views");
            DropTable("dbo.GithubProjectViews");
            DropTable("dbo.GithubProjects");
        }
    }
}
