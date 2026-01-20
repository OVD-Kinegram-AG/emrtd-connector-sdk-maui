using System.Resources;

public class LocalizedStrings
{
    public string? this[string key] => _resourceManager.GetString(key);

    private readonly ResourceManager _resourceManager;

    public LocalizedStrings()
    {
        _resourceManager = new ResourceManager(
            "EmrtdConnectorMaui.Resources.Locales.Strings",
            typeof(LocalizedStrings).Assembly
        );
    }
}