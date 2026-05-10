using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Abstraction;
using Shared.Common;

namespace E_Commerce.API.Extentions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();
            services.Configure<JwtOptions>(configuration.GetSection("JWTOptions"));
            return services;
        }
    }
}
