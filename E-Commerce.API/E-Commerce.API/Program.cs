using E_Commerce.API.Extentions;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region DI Container
            // Add WepApi Service
            builder.Services.WebApiExtension();

            // Add Infrastructure Service
            builder.Services.AddInfrastructureExtension(builder.Configuration);

            // Add Core Service
            builder.Services.AddCoreExtension(builder.Configuration);
            #endregion

            #region Pipelines - Middlewares
            var app = builder.Build();

            await app.SeedDatabaseAsync();

            app.UseExceptionsHandlingMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
