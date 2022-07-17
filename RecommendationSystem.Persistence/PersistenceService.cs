using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecommendationSystem.Application.Interfaces;
using RecommendationSystem.Persistence.Repositories;
using RecommendationSystem.Persistence.Utils;

namespace RecommendationSystem.Persistence
{
    public static class PersistenceService
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbPostgreSQLContext<RecommendationSystemDbContext>(connectionString);

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IUserPredictionRepository, UserPredictionRepository>();
            services.AddScoped<IUserRatingRepository, UserRatingRepository>();
            services.AddScoped<IUserPredictionGroupRepository, UserPredictionGroupRepository>();

            services.AddSingleton<ITmdbMovieConnector, TmdbConnector>();

            return services;
        }
        private static IServiceCollection AddDbPostgreSQLContext<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(T).Assembly.FullName))
                .UseSnakeCaseNamingConvention());

            return services;
        }
    }
}
