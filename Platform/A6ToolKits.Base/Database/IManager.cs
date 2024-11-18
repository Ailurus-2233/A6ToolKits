namespace A6ToolKits.Database;

/// <summary>
///     数据库管理器
/// </summary>
public interface IManager
{
    void Save(IList<IData> data);
    IList<IData> Load(IData target);
    void Delete(IData target);
    void Update(IData target);
}