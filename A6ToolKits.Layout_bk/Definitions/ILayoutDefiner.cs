namespace A6ToolKits.Layout.Definitions;

public interface ILayoutDefiner
{
    public IDefiner MenuDefiner { get; set; }
    public IDefiner ToolbarDefiner { get; set; }
    public IDefiner StatusBarDefiner { get; set; }
    public IDefiner PageDefiner { get; set; }
}