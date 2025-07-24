using Foxy.DataLayer.DBContext;
using Foxy.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ApiIntegrationTest;

public class FoxyWebApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<FoxyDbContext>));
                var connString = GetConnectionString();

                services.AddDbContext<FoxyDbContext>(options =>
                    options.UseNpgsql(connString));

                var dbContext = CreateDbContext(services);
                dbContext.Database.EnsureCreated();
            });
        });
    }

    private static string? GetConnectionString()
    {
        return "Host=localhost;Port=5432;Database=foxy_webapi;Username=postgres;Password=postgres";
    }

    private static FoxyDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FoxyDbContext>();
        return dbContext;
    }
}