using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Reflection;

namespace Blazor.Extensions.Presentation.Components;

public class CopyrightComponent : ComponentBase
{
    protected static string Copyright
    {
        get
        {
            var assembly = Assembly.GetExecutingAssembly();
            var copyright = FileVersionInfo.GetVersionInfo(assembly.Location).LegalCopyright ?? string.Empty;
            return copyright;
        }
    }
}
