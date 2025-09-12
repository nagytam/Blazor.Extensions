using Blazor.Extensions.Components.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Blazor.Extensions.App.Server.Components;

public class AppComponent : ComponentBase
{
    [CascadingParameter]
    public HttpContext? HttpContext { get; set; }

    protected override void OnInitialized()
    {
        LocalizationHelper.AddLocalizationCookie(HttpContext);
    }
}
