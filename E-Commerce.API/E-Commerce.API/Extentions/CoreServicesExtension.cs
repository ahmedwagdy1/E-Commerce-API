using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Abstraction;
using Services.Abstraction.Contracts;
using Services.Implementations;
using Shared.Common;

namespace E_Commerce.API.Extentions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManger, ServiceMangerWithFactoryService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>());

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<Func<IPaymentService>>(provider =>
            () => provider.GetRequiredService<IPaymentService>());

            services.AddScoped<ICashService,  CashService>();
            services.AddScoped<Func<ICashService>>(provider =>
            () => provider.GetRequiredService<ICashService>());

            services.Configure<JwtOptions>(configuration.GetSection("JWTOptions"));
            return services;
        }
    }
}
