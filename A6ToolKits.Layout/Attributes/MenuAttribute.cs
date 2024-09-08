namespace A6ToolKits.Layout.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MenuAttribute : Attribute
{
    public GroupInfo[] Path { get; set; }

    public MenuAttribute(params string[] path)
    {
        Path = path.Select(p =>
        {
            var split = p.Split(':');
            return new GroupInfo(split[0], int.Parse(split[1]));
        }).ToArray();
    }
}

public readonly struct GroupInfo(string itemName, int itemValue)
{
    public string ItemName { get; } = itemName;
    public int Order { get; } = itemValue;
    
    public override string ToString() => $"{ItemName}:{Order}";
}