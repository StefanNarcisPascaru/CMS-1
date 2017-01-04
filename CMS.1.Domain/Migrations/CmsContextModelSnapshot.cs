using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CMS.Domain;

namespace CMS._1.Domain.Migrations
{
    [DbContext(typeof(CmsContext))]
    partial class CmsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMS.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("message");

                    b.Property<string>("subject");

                    b.Property<string>("username");

                    b.HasKey("UserId", "message");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CMS.Domain.Models.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("CMS.Domain.Models.Resource", b =>
                {
                    b.Property<string>("path")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("type");

                    b.HasKey("path");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("CMS.Domain.Models.Subject", b =>
                {
                    b.Property<string>("subjectName");

                    b.Property<string>("teacherName");

                    b.Property<Guid>("Id");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("subjectName", "teacherName");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("CMS.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompleteName");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CMS.Domain.Models.UserRank", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RankId");

                    b.HasKey("UserId", "RankId");

                    b.HasIndex("RankId");

                    b.ToTable("UserRanks");
                });

            modelBuilder.Entity("CMS.Domain.Models.Comment", b =>
                {
                    b.HasOne("CMS.Domain.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMS.Domain.Models.UserRank", b =>
                {
                    b.HasOne("CMS.Domain.Models.Rank", "Rank")
                        .WithMany("UserRanks")
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMS.Domain.Models.User", "User")
                        .WithMany("UserRanks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
