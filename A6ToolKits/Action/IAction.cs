using System;
using System.Threading.Tasks;

namespace A6ToolKits.Action;

// 动作接口，可以异步执行一个动作
public interface IAction
{
    public bool CanRun { get; set; }

    public EventHandler? CanRunChanged { get; set; }

    public Task Run();
}