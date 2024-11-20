using A6ToolKits.Database.DataModel;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     数据库管理器基类
/// </summary>
public abstract class DatabaseManagerBase: IManager
{
    /// <summary>
    ///     数据库类型
    /// </summary>
    protected abstract string ConnectionString { get; }
    
    /// <summary>
    ///     将数据保存到数据库
    /// </summary>
    /// <param name="data"></param>
    public abstract void Save(IList<IData> data);
    
    /// <summary>
    ///     数据库类型
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public abstract IList<IData> Load(IData target);
    
    /// <summary>
    ///     从数据库中删除数据
    /// </summary>
    /// <param name="target">
    ///     删除的目标
    /// </param>
    public abstract void Delete(IData target);
    
    
    /// <summary>
    ///     更新数据库中的数据
    /// </summary>
    /// <param name="target">
    ///     更新的目标
    /// </param>
    public abstract void Update(IData target);
}