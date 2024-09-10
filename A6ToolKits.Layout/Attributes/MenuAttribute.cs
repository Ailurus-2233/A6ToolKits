namespace A6ToolKits.Layout.Attributes;

/// <summary>
///     菜单属性，标记到一个 ActionBase 的属性上，用于生成 MenuItem
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MenuAttribute : Attribute
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="path">
    ///     菜单路径，格式为 "Group:Order"，如 "File:1"
    /// </param>
    public MenuAttribute(params string[] path)
    {
        Path = path.Select(p =>
        {
            var split = p.Split(':');
            return new GroupInfo(split[0], int.Parse(split[1]));
        }).ToArray();
    }

    /// <summary>
    ///     菜单路径，生成 MenuItem 时，根据路径将 MenuItem 放置到对应的位置
    /// </summary>
    public GroupInfo[] Path { get; set; }
}

/// <summary>
///     菜单路径信息
/// </summary>
/// <param name="itemName">
///     所在菜单的名称
/// </param>
/// <param name="itemValue">
///     在菜单中的排序值
/// </param>
public readonly struct GroupInfo(string itemName, int itemValue)
{
    /// <summary>
    ///     菜单名称
    /// </summary>
    public string ItemName { get; } = itemName;

    /// <summary>
    ///     菜单排序值
    /// </summary>
    public int Order { get; } = itemValue;
}