using Avalonia;
using Avalonia.Markup.Xaml;

namespace A6Application;

/// <summary>
///     The application class.
/// </summary>
public class App : Application
{
    /// <summary>
    ///     Initialize the application.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}