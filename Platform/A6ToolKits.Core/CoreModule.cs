using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Module;

namespace A6ToolKits;

/// <summary>
///     核心模块
/// </summary>
[AutoRegister(typeof(CoreModule), RegisterType.Singleton)]
public class CoreModule : ModuleBase
{
    /// <summary>
    ///     实例创建器，用于模块内部创建实例
    /// </summary>
    public override IInstanceHelper? Creator { get; set; } = new BaseInstanceHelper();
    
    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    protected override void Initialize()
    {

    }
}