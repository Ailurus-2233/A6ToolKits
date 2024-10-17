using A6ToolKits;
using Avalonia;
using Avalonia.Themes.Fluent;
namespace UIDemo;

public class App : Application {
    public override void Initialize() {
        base.Initialize();
        Styles.Add(new FluentTheme());
    }
}