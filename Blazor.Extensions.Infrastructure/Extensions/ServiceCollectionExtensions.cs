using Blazor.Extensions.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Extensions.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringApplication = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionStringApplication));

        services.AddDbContext<BlazorDbContext>(options =>
            options.UseInMemoryDatabase("BlazorDatabaseName"));

        services.AddDatabaseDeveloperPageExceptionFilter(); // TODO Development only
    }
}
