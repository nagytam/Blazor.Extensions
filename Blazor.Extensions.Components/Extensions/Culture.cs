namespace Blazor.Extensions.Components.Extensions;

public class Culture
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DisplayName { get; set; }

    public Culture(string name, string description, string displayName)
    {
        Name = name;
        Description = description;
        DisplayName = displayName;
    }
}
