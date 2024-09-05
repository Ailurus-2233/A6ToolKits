using System;
using System.Threading.Tasks;
using A6Application.Views;
using A6ToolKits.Command;
using A6ToolKits.Helper;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6Application.Commands;

public class OpenWindowCommand : CommandDefinition
{
    public override string Text => "Open Window";
    public override string ToolTip => "Open a new TestWindow";
    public override IImage ImageSource => 
        ImageHelper.LoadFromResource(new Uri("avares://A6Application/Assets/avalonia-logo.ico"));

    public override Task Run()
    {
        var testWindow = new TestWindow();
        testWindow.Show();
        return Task.CompletedTask;
    }
}