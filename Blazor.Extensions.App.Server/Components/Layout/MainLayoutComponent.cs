using Microsoft.AspNetCore.Components;

namespace Blazor.Extensions.App.Server.Components.Layout;

public class MainLayoutComponent : LayoutComponentBase
{
    protected List<string> Languages = ["en-US", "de-CH"];
    protected string SelectedLanguage = "en-US";
    protected void OnLanguageChanged(string args)
    {
        throw new NotImplementedException();
    }
}
