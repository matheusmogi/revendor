using Microsoft.Extensions.DependencyInjection;
using Revendor.Application.Authentication;
using Revendor.Application.Services;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ITokenIssuer,TokenIssuer>();
            services.AddScoped<ICustomerService,CustomerService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IBrandService,BrandService>();
            return services;
        }
    }
}