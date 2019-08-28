using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GithubStatisticsCore.Migrations
{
    public partial class Github : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GithubProjects",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Fork = table.Column<short>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StargazersCount = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    ForkCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GithubProjects", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "GithubProjectViews",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    TotalCount = table.Column<int>(nullable: false),
                    TotalUniques = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GithubProjectViews", x => x.Name);
                    table.ForeignKey(
                        name: "FK_GithubProjectViews_GithubProjects_Name",
                        column: x => x.Name,
                        principalTable: "GithubProjects",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "View",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Uniques = table.Column<int>(nullable: false),
                    GithubProjectViewName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View", x => x.Id);
                    table.ForeignKey(
                        name: "FK_View_GithubProjectViews_GithubProjectViewName",
                        column: x => x.GithubProjectViewName,
                        principalTable: "GithubProjectViews",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_View_GithubProjectViewName",
                table: "View",
                column: "GithubProjectViewName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "View");

            migrationBuilder.DropTable(
                name: "GithubProjectViews");

            migrationBuilder.DropTable(
                name: "GithubProjects");
        }
    }
}
