using Blazor.Extensions.Domain.Helpers;
using Microsoft.AspNetCore.Components;

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
