using System.Threading.Tasks;
using A6ToolKits.Common.Attributes;
using A6ToolKits.Helper.ControlGenerator;
using Avalonia.Controls;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable VirtualMemberNeverOverridden.Global

namespace A6ToolKits.Command;

/// <summary>
/// </summary>
public abstract class CommandBase : CommandDefinitionBase, ICommandHandler
{
    private bool _enable = true;
    private bool _visible = true;

    /// <summary>
    ///     是否可见
    /// </summary>
    public bool Visible
    {
        get => _visible;
        set => SetField(ref _visible, value);
    }

    /// <summary>
    ///     是否可用
    /// </summary>
    public bool Enable
    {
        get => _enable;
        set => SetField(ref _enable, value);
    }

    /// <summary>
    ///     更新 Command
    /// </summary>
    public void Update()
    {
    }

    /// <summary>
    ///     异步运行 Command
    /// </summary>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    public abstract Task Run();

    /// <summary>
    ///     生成一个可以运行命令的控件
    /// </summary>
    /// <param name="commandControlType">
    ///     控件类型
    /// </param>
    /// <param name="height">
    ///     控件高度
    /// </param>
    /// <returns>
    ///     返回的控件
    /// </returns>
    public Control CreateControl(CommandControlType commandControlType, double height)
    {
        return commandControlType switch
        {
            CommandControlType.TextMenuItem => this.GenerateMenuItem(MenuItemType.Text, height),
            CommandControlType.IconAndTextMenuItem => this.GenerateMenuItem(MenuItemType.IconAndText, height),
            CommandControlType.IconButton => this.GenerateButton(ButtonType.Icon, height),
            CommandControlType.TextButton => this.GenerateButton(ButtonType.Text, height),
            CommandControlType.IconAndTextButton => this.GenerateButton(ButtonType.IconAndText, height),
            _ => new Control()
        };
    }
}