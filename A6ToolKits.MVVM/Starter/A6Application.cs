using System.Reflection;
using A6ToolKits.MVVM.Container;
using A6ToolKits.MVVM.Starter.MainPage;
using Avalonia;
using Avalonia.Controls;
using Prism.DryIoc;

namespace A6ToolKits.MVVM.Starter;

public abstract class A6Application : PrismApplication
{
    protected abstract string Title { get; }

    protected override void RegisterTypes(IContainerRegistry container)
    {
        Starter.RegisterTypes(container);
    }

    protected override AvaloniaObject CreateShell()
    {
        Starter.Title = Title;
        return Starter.GetStartInstance();
    }

    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();
        Starter.ConfigureViewModelLocator();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        Starter.LoadModule(moduleCatalog);
    }
    
    public virtual void SetMainView(UserControl view)
    {
        Starter.SetDisplayView(view);
    }
}