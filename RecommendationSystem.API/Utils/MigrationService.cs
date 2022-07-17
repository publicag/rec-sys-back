using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecommendationSystem.Identity;
using RecommendationSystem.Persistence;

namespace RecommendationSystem.API.Utils
{
    public static class MigrationService
    {
        public static IApplicationBuilder UseMigrationService(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<RecommendationSystemDbContext>();
            var identityContext = serviceScope.ServiceProvider.GetRequiredService<RecommendationSystemIdentityDbContext>();
            context.Database.Migrate();
            identityContext.Database.Migrate();

            return app;
        }
    }
}
