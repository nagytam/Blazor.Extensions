using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

namespace Blazor.Extensions.Presentation.Helpers;

public static class SyncfusionHelper
{
    public static IServiceCollection AddBlazorExtension(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSyncfusionBlazor();
    }

    public static void Initialize()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXZfcnVWRGlfUE1+WkZWYEg=");
    }
}
