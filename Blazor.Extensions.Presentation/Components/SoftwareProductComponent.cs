using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Reflection;

namespace Blazor.Extensions.Presentation.Components;

public class SoftwareProductComponent : ComponentBase
{
    protected static string Product
    {
        get
        {
            var assembly = Assembly.GetExecutingAssembly();
            var productName = FileVersionInfo.GetVersionInfo(assembly.Location).ProductName ?? string.Empty;
            return productName;
        }
    }
}
