using System.Globalization;
using System.Resources;

namespace ToDoList;

public static class LanguageManager
{
    private static ResourceManager? _resourceManager;
    private static CultureInfo? _currentCulture;

    public static void InitialResource(string languageCode)
    {
        _resourceManager = new ResourceManager($"ToDoList.Languages.{languageCode}", typeof(LanguageManager).Assembly);
        _currentCulture = CultureInfo.GetCultureInfo(languageCode);
    }
    
    public static string? GetString(string key)
    {
        return _resourceManager?.GetString(key, _currentCulture);
    }
}