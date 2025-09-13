namespace Blazor.Extensions.Domain.Model;

public static class Cultures
{
    public static string DefaultCultureName = "en-US";
    public static Culture[] SupportedCultures = 
    [ 
        new Culture("en-US", "English", "English"),
        new Culture("de-CH", "Deutsch (German)", "Deutsch"),
        new Culture("fr-CH", "Français (French)", "Français"),
        new Culture("it-CH", "Italiano (Italian)", "Italiano")
    ];
}
