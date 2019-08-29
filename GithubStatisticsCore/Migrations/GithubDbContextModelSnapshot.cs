﻿// <auto-generated />
using System;
using GithubStatisticsCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GithubStatisticsCore.Migrations
{
    [DbContext(typeof(GithubDbContext))]
    partial class GithubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GithubStatisticsCore.Models.GithubProject", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<bool>("Fork");

                    b.Property<int>("ForkCount");

                    b.Property<string>("Language");

                    b.Property<string>("StargazersCount");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Url");

                    b.HasKey("Name");

                    b.ToTable("GithubProjects");
                });

            modelBuilder.Entity("GithubStatisticsCore.Models.GithubProjectView", b =>
                {
                    b.Property<string>("Name");

                    b.Property<int>("TotalCount");

                    b.Property<int>("TotalUniques");

                    b.HasKey("Name");

                    b.ToTable("GithubProjectViews");
                });

            modelBuilder.Entity("GithubStatisticsCore.Models.View", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("GithubProjectViewName");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Uniques");

                    b.HasKey("Id");

                    b.HasIndex("GithubProjectViewName");

                    b.ToTable("View");
                });

            modelBuilder.Entity("GithubStatisticsCore.Models.GithubProjectView", b =>
                {
                    b.HasOne("GithubStatisticsCore.Models.GithubProject", "GithubProject")
                        .WithMany()
                        .HasForeignKey("Name")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GithubStatisticsCore.Models.View", b =>
                {
                    b.HasOne("GithubStatisticsCore.Models.GithubProjectView", "GithubProjectView")
                        .WithMany("Views")
                        .HasForeignKey("GithubProjectViewName");
                });
#pragma warning restore 612, 618
        }
    }
}
