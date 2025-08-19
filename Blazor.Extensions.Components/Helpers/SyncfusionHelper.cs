using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

namespace Blazor.Extensions.Components.Helpers;

public static class SyncfusionHelper
{
    public static IServiceCollection AddBlazorExtension(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSyncfusionBlazor();
    }

    public static void Initialize()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXdfeXRdQ2RZWEJ2W0BWYEk=");
    }
}
