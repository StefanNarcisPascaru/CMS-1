using System;
using System.Linq;
using CMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Domain
{
    public class CmsContext : DbContext//IdentityDbContext<User,Rank,Guid>
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<UserRank> UserRanks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Resource> Resources { get; set; }


        public void Seed()
        {
            if (!Ranks.Any())
            {
                Ranks.Add(new Rank() { Id = Guid.Parse("19BF8D19A9C640CBBB3DA229A1728D2B"), Name = "Professor" });
                Ranks.Add(new Rank() { Id = Guid.Parse("E10601DC-DEB3-4D33-A113-A7CCE86B145B"), Name = "Student" });
                Ranks.Add(new Rank() { Id = Guid.Parse("CB8FEF1A-1130-41D2-A371-91B4F004917A"), Name = "Admin" });
                SaveChanges();
            }
            if (!Users.Any())
            {
                Users.Add(new User()
                {
                    Id = Guid.Parse("EBF73255-1252-4FD0-AAD5-7C9D61E38824"),
                    CompleteName = "Grivei",
                    UserName = "grivei",
                    Email = "grivei@mail.com",
                    Password = "griveiNo.1"
                });
                UserRanks.Add(new UserRank()
                {
                    UserId = Guid.Parse("EBF73255-1252-4FD0-AAD5-7C9D61E38824"),
                    RankId = Guid.Parse("19BF8D19A9C640CBBB3DA229A1728D2B")
                });

                Users.Add(new User()
                {
                    Id = Guid.Parse("1A684DE2-CE5E-487E-A419-68C777B29831"),
                    CompleteName = "Azorel",
                    UserName = "azorel",
                    Email = "azorel@mail.com",
                    Password = "azorelNo.1"
                });
                UserRanks.Add(new UserRank()
                {
                    UserId = Guid.Parse("1A684DE2-CE5E-487E-A419-68C777B29831"),
                    RankId = Guid.Parse("CB8FEF1A-1130-41D2-A371-91B4F004917A")
                });
                SaveChanges();
            }

            if (!Subjects.Any())
            {
                Subjects.Add(new Subject()
                {
                    Id = Guid.Parse("c5a5d946-2db0-49bf-b008-da51382802b9"),
                    subjectName = "POO",
                    teacherName = "Gavrilut"

                });
                SaveChanges();
            }

            if (!Resources.Any())
            {

                Resources.Add(new Resource()
                {
                    SubjectNo = Guid.Parse("c5a5d946-2db0-49bf-b008-da51382802b9"),
                    type = "Course",
                    path = " ..//.. "
                });
                SaveChanges();

            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source = (localdb)\\MSSQLLocalDB;Initial Catalog=CMS;Integrated Security=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserRank>().HasKey(ur => new { ur.UserId, ur.RankId });
            // modelBuilder.Entity<Subject>().HasKey(s => new { s.subjectName, s.teacherName });
            modelBuilder.Entity<Resource>().HasKey(r => new { r.path });
            modelBuilder.Entity<Comment>().HasKey(c => new { c.UserId, c.message });

            modelBuilder.Entity<User>().HasMany(u => u.Comments);
            modelBuilder.Entity<User>().HasMany(u => u.UserRanks);
            modelBuilder.Entity<Rank>().HasMany(r => r.UserRanks);
            modelBuilder.Entity<Subject>().HasMany(s => s.Resources);
        }
    }
}
