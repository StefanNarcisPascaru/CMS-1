using System;
using System.Linq;
using CMS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Domain
{
    public class CmsContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<UserRank> UserRanks { get; set; }

        public void  Seed()
        {
            if (!Ranks.Any())
            {
                Ranks.Add(new Rank() {Id = Guid.Parse("19BF8D19A9C640CBBB3DA229A1728D2B"), Name = "Professor"});
                Ranks.Add(new Rank() {Id = Guid.Parse("E10601DC-DEB3-4D33-A113-A7CCE86B145B"), Name = "Student"});
                SaveChanges();
                Dispose();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CMS;Integrated Security=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRank>().HasKey(ur => new {ur.UserId, ur.RankId});
            modelBuilder.Entity<User>().HasMany(u => u.UserRanks);
            modelBuilder.Entity<Rank>().HasMany(r => r.UserRanks);
        }
    }
}
