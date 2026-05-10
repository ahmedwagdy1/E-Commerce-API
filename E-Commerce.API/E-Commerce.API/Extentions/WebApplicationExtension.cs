using Domain.Contracts;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extentions
{
    public static class WebApplicationExtension
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var objectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objectOfDataSeeding.SeedDataAsync();
            await objectOfDataSeeding.SeedIdentityDataAsync();
            return app;
        }
        public static WebApplication UseExceptionsHandlingMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionsHandlingMiddleware>();
            return app;
        }
        public static WebApplication UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
