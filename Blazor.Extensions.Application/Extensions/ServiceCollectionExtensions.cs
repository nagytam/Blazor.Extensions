using Blazor.Extensions.Application.Services;
using Blazor.Extensions.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Extensions.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<FileService>();
    }
}
