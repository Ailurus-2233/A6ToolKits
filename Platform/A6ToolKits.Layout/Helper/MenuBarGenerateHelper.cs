using System.Reflection;
using A6ToolKits.Assembly;
using A6ToolKits.Commands;
using A6ToolKits.Commands.ControlGenerators;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Exceptions;
using A6ToolKits.Layout.Generator;
using Avalonia.Controls;
namespace A6ToolKits.Layout.Helper;

/// <summary>
///     菜单生成工具
/// </summary>
internal static class MenuBarGenerateHelper
{
    private static WindowConfig _config { get; set; } = WindowConfig.Instance;
    private static WindowGenerator _generator { get; set; } = WindowGenerator.Instance;
    
    /// <summary>
    ///     获取所有有MenuAction属性的ActionBase类
    /// </summary>
    /// <returns>
    ///     分组列表，组的Key是分组名称，Value是类型
    /// </returns>
    public static List<IGrouping<string, Type>> GetMenuItemGroup()
    {
        var types = AssemblyHelper.GetTypeWithAttribute<MenuActionAttribute>();
        var group = types.GroupBy(t => GetMenuActionAttribute(t).Path[0].ItemName);
        var result = group.OrderBy(g => GetMenuActionAttribute(g.First()).Path[0].Order).ToList();
        return result;
    }

    /// <summary>
    ///     按照分组，添加菜单子项到 MenuItem
    /// </summary>
    /// <param name="menuItem">待添加子项的菜单项</param>
    /// <param name="children">子项字典，Key为 int 类型，记录分组ID，并根据ID排序</param>
    public static void AddChildren(this MenuItem menuItem, Dictionary<int, List<MenuItem>> children)
    {
        var keys = children.Keys.ToList();
        keys.Sort();
        keys.ForEach(key =>
        {
            children[key].ForEach(item => menuItem.Items.Add(item));
            menuItem.Items.Add(new Separator());
        });
        menuItem.Items.RemoveAt(menuItem.Items.Count - 1);
    }

    /// <summary>
    ///     获取类的 MenuActionAttribute 属性
    /// </summary>
    /// <param name="type" cerf="Type"> 目标类 </param>
    /// <returns cref="MenuActionAttribute"> MenuActionAttribute 对象 </returns>
    /// <exception cref="TypeNotFoundException"> </exception>
    public static MenuActionAttribute GetMenuActionAttribute(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        var attribute = type.GetCustomAttribute<MenuActionAttribute>();
        if (attribute == null) throw new AttributeNotFoundException(type, typeof(MenuActionAttribute));
        return attribute;
    }

    /// <summary>
    ///     生成当前菜单项的子项
    /// </summary>
    /// <param name="level">
    ///     菜单项的层数，例如 文件>打开>打开文件夹，这个层级就是3
    /// </param>
    /// <param name="group">
    ///     GetMenuItemGroup()获得的菜单组
    /// </param>
    /// <returns></returns>
    public static Dictionary<int, List<MenuItem>> GenerateChildren(int level, IGrouping<string?, Type> group)
    {
        var dict = new Dictionary<int, List<MenuItem>>();

        var types = group.Where(type => GetMenuActionAttribute(type).Path.Length == level);
        var groups = group.Where(type => GetMenuActionAttribute(type).Path.Length > level)
            .GroupBy(type => GetMenuActionAttribute(type).Path[level].ItemName);

        foreach (var next in groups)
        {
            var menuItem = new MenuItem
            {
                Header = next.Key
            };

            var children = GenerateChildren(level + 1, next);
            menuItem.AddChildren(children);

            var key = GetMenuActionAttribute(next.First()).Path[level - 1].Order;
            AddKeyValue(key, menuItem);
        }

        foreach (var type in types)
        {
            // 判断 type 是否是 CommandBase 的子类
            if (!typeof(CommandBase).IsAssignableFrom(type)) 
                throw new GenerateTypeNotEqualException(type.ToString(), typeof(CommandBase).ToString());
            var obj = _generator.Creator?.Create(type);
            if (obj is not CommandBase temp) continue;
            var menuItemType = type.GetMenuActionAttribute().Type;
            var menuItem = temp.GenerateMenuItem(menuItemType, _config.MenuHeight);
            var key = GetMenuActionAttribute(type).Path[level - 1].Order;
            AddKeyValue(key, menuItem);
        }

        return dict;

        void AddKeyValue(int key, MenuItem value)
        {
            if (!dict.TryGetValue(key, out var list))
            {
                list = [];
                dict[key] = list;
            }
            list.Add(value);
        }
    }

    /// <summary>
    ///     生成所有的菜单项
    /// </summary>
    /// <returns>
    ///     菜单项列表
    /// </returns>
    public static List<MenuItem> GenerateMenuItems()
    {
        var result = new List<MenuItem>();
        var groups = GetMenuItemGroup();
        if (groups.Count == 0) return result;

        groups.ForEach(group =>
        {
            var menuItem = new MenuItem
            {
                Header = group.Key,
            };
            var dict = GenerateChildren(1, group);
            menuItem.AddChildren(dict);
            result.Add(menuItem);
        });
        return result;
    }
}