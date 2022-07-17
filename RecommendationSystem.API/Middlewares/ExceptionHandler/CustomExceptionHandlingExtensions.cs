using Microsoft.AspNetCore.Builder;

namespace RecommendationSystem.API.Middlewares.ExceptionHandler
{
    public static class CustomExceptionHandlingExtensions
    {
        public static void UseCustomExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
