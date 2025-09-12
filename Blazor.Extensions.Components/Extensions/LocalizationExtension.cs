using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Blazor.Extensions.Components.Extensions;

public static class LocalizationExtension
{
    public static IServiceCollection AddLocalizationExtension(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Locales");

        return services;
    }

    public static void SetDefaultAndSupportedCultures(this IApplicationBuilder applicationBuilder, Culture[] supportedCultures, string defaultCultureName)
    {
        ArgumentNullException.ThrowIfNull(applicationBuilder);
        ArgumentNullException.ThrowIfNull(supportedCultures);
        Debug.Assert(supportedCultures.Length > 0, "At least one supported culture must be provided.");

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(defaultCultureName)
            .AddSupportedCultures([.. supportedCultures.Select(c => c.Name)])
            .AddSupportedUICultures([.. supportedCultures.Select(c => c.Name)]);
        applicationBuilder.UseRequestLocalization(localizationOptions);
    }
}
