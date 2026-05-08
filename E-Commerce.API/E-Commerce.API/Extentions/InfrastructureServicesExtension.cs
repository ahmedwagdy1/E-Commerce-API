using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using StackExchange.Redis;

namespace E_Commerce.API.Extentions
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });
            return services;
        }
    }
}
