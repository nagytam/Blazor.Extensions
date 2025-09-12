using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Blazor.Extensions.Components.Components;

public partial class LanguageSelectorComponent : ComponentBase
{
    [Inject]
    public required NavigationManager Navigation { get; set; }

    public string SelectedLanguage 
    {
        get 
        {
            return CultureInfo.CurrentCulture.Name;
        }
        set 
        {
            if (!string.IsNullOrEmpty(value) && CultureInfo.CurrentCulture.Name != value)
            {
                var uri = new Uri(Navigation.Uri)
                    .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                var cultureEscaped = Uri.EscapeDataString(value);
                var uriEscaped = Uri.EscapeDataString(uri);

                var fullUri = $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}";
                Navigation.NavigateTo(fullUri, forceLoad: true);
            }
        }
    }
}
