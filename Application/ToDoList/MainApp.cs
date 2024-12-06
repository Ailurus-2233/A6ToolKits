using Avalonia;
using Avalonia.Themes.Fluent;

namespace ToDoList;

public class MainApp : Application
{
    public override void Initialize() {
        base.Initialize();
        Styles.Add(new FluentTheme());
    }
}