using Avalonia;
using Avalonia.Markup.Xaml;

namespace A6Application;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}