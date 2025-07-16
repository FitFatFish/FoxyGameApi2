using Foxy.DataLayer.DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace ApiIntegrationTest;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            // Remove the app's DbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<FoxyDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Get the original connection string from configuration
            var configuration = context.Configuration;
            var originalConnStr = configuration.GetConnectionString("DefaultConnection");

            // Parse and modify the database name to prefix with "_test"
            var builder = new NpgsqlConnectionStringBuilder(originalConnStr);
            builder.Database = "test_" + builder.Database;

            // Add DbContext using PostgreSQL for testing
            services.AddDbContext<FoxyDbContext>(options =>
            {
                options.UseNpgsql(builder.ConnectionString);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (FoxyDbContext).
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FoxyDbContext>();
                db.Database.EnsureCreated();
            }
        });
    }
}