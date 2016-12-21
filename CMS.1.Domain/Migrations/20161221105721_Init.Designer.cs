using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CMS.Domain;

namespace CMS.Domain.Migrations
{
    [DbContext(typeof(CmsContext))]
    [Migration("20161221105721_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMS.Domain.Models.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
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
