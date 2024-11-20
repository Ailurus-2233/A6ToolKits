using A6ToolKits.Database.DataModel;

namespace A6ToolKits.Database;

/// <summary>
///     数据库管理器
/// </summary>
public interface IManager
{
    /// <summary>
    ///     保存数据
    /// </summary>
    /// <param name="data"></param>
    void Save(IList<IData> data);


    /// <summary>
    ///     从数据库中加载数据
    /// </summary>
    /// <param name="target">
    ///     加载的目标
    /// </param>
    /// <returns>
    ///     加载的数据
    /// </returns>
    IList<IData> Load(IData target);


    /// <summary>
    ///    从数据库中删除数据
    /// </summary>
    /// <param name="target">
    ///     删除的目标
    /// </param>
    void Delete(IData target);


    /// <summary>
    ///     更新数据库中的数据
    /// </summary>
    /// <param name="target">
    ///     更新的目标
    /// </param>
    void Update(IData target);
}