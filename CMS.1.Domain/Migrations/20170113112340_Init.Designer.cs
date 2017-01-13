using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CMS.Domain;

namespace CMS._1.Domain.Migrations
{
    [DbContext(typeof(CmsContext))]
    [Migration("20170113112340_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("SubjectNo");

                    b.Property<string>("type");

                    b.HasKey("path");

                    b.HasIndex("SubjectNo");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("CMS.Domain.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("subjectName");

                    b.Property<string>("teacherName");

                    b.HasKey("Id");

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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RankId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RankId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRanks");
                });

            modelBuilder.Entity("CMS.Domain.Models.Comment", b =>
                {
                    b.HasOne("CMS.Domain.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMS.Domain.Models.Resource", b =>
                {
                    b.HasOne("CMS.Domain.Models.Subject", "Subject")
                        .WithMany("Resources")
                        .HasForeignKey("SubjectNo")
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
