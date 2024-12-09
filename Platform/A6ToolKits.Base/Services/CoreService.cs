using A6ToolKits.Bootstrapper.Events;
using A6ToolKits.Common.Container;
using A6ToolKits.Container.Attributes;
using A6ToolKits.EventAggregator;

namespace A6ToolKits.Services;

/// <summary>
///     核心服务
/// </summary>
[AutoRegister(typeof(CoreService), RegisterType.Singleton)]
public class CoreService : IService
{
    /// <inheritdoc />
    public void Initialize()
    {
        var serviceList = IoC.GetInstance<IServiceManager>()?.GetToLoadServiceList();
        if (serviceList is not { Count: > 0 }) return;
        var eventAggregator = IoC.GetInstance<IEventAggregator>();

        foreach (var service in serviceList)
        {
            // 判断 module 是否是 ModuleBase 的基类
            if (!service.GetInterfaces().Contains(typeof(IService))) return;
            var target = IoC.GetInstance(service) as IService;
            target?.Initialize();
            eventAggregator?.Subscribe<ApplicationExitEvent>(e => target?.OnExit());
        }
    }

    /// <inheritdoc />
    public void OnExit()
    {
        // TODO 退出时的操作
    }
}