using Services;
using Services.Abstraction;

namespace E_Commerce.API.Extentions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreExtension(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();
            return services;
        }
    }
}
