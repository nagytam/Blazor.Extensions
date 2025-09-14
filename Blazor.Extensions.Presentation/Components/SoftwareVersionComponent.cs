using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Blazor.Extensions.Presentation.Components;

public class SoftwareVersionComponent : ComponentBase
{
    protected static string Version
    {
        get
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            var softwareVersion = $"{version?.Major:0000}.{version?.Minor:00}.{version?.Build:00000}"; // .{version?.Revision:00}
            return $"Version: {softwareVersion}";
        }
    }
}
