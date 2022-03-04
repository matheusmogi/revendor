using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revendor.Domain.Interfaces;
using Revendor.Infrastructure.Persistence;

namespace Revendor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Values:DefaultConnection"];
            services.AddScoped(provider => new RevendorContext(connectionString));
            services.AddScoped<IDateTime, DateTimeService>();
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}