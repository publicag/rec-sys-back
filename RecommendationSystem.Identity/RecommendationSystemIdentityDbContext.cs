using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecommendationSystem.Identity.Entities;

namespace RecommendationSystem.Identity
{
    public class RecommendationSystemIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;

        public RecommendationSystemIdentityDbContext(DbContextOptions<RecommendationSystemIdentityDbContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
