using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecommendationSystem.Identity.Entities;

namespace RecommendationSystem.Identity
{
    public static class IdentityService
    {
        private static void AddDbContext<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(typeof(T).Assembly.FullName)).UseSnakeCaseNamingConvention());
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecommendationSystemIdentityDbContext>(connectionString);
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<RecommendationSystemIdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
