﻿using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RecommendationSystem.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
