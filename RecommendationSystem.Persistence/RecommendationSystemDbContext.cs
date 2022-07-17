using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence
{
    public class RecommendationSystemDbContext : DbContext
    {
        public RecommendationSystemDbContext(DbContextOptions<RecommendationSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<UserPrediction> UserPred { get; set; }
        public DbSet<UserRating> UserRating { get; set; }
        public DbSet<UserPredGroup> UserPredGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateTime = DateTime.Now;
                        entry.Entity.ModifiedDateTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDateTime = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
