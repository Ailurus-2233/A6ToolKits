namespace A6ToolKits.Layout.Attributes;

public class PageAttribute(string pageName)
{
    public string PageName { get; set; } = pageName;
}