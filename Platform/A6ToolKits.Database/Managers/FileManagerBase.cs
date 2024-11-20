using A6ToolKits.Database.DataModel;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     数据库管理器
/// </summary>
public abstract class FileManagerBase : IManager
{
    /// <summary>
    ///     文件夹路径
    /// </summary>
    protected abstract string FolderPath { get; }
    
    /// <summary>
    ///     将数据保存到数据库
    /// </summary>
    /// <param name="data">
    ///     数据列表
    /// </param>
    public abstract void Save(IList<IData> data);
    
    /// <summary>
    ///     从数据库中加载数据
    /// </summary>
    /// <param name="target">
    ///     加载的目标
    /// </param>
    /// <returns>
    ///     加载的数据列表
    /// </returns>
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