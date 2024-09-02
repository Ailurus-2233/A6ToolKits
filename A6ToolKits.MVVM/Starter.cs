using System.Reflection;
using A6ToolKits.MVVM.Common;

namespace A6ToolKits.MVVM;

public abstract class Starter
{
    public void Run()
    {
        Initialize();
        ConfigureAssembly();
    }
    
    protected virtual void Initialize()
    {
        
    }
    
    protected virtual void ConfigureAssembly()
    {
        var assembly = Assembly.GetEntryAssembly();
        if (assembly != null) IoCHelper.AutoRegister(assembly);
    }
    
}