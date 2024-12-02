using A6ToolKits.Database.DataModel;
using A6ToolKits.Database.Exceptions;
using System.Xml;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     数据库管理器
/// </summary>
public abstract class FileManagerBase : IManager<FileDataModelBase>
{
    /// <summary>
    ///     锁对象，在多线程环境下使用
    /// </summary>
    protected object Locker = new();

    /// <summary>
    ///     文件夹路径
    /// </summary>
    protected abstract string FolderPath { get; }

    /// <summary>
    ///     加载数据集的索引
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <returns>数据集的索引列表</returns>
    protected IList<int> LoadIndex<T>() where T : FileDataModelBase
    {
        var dataset = Load<T>();
        List<int> result = [];
        result.AddRange(dataset.Select(item => item.GetHashCode()));
        return result;
    }

    /// <summary>
    ///     生成数据存储的文件
    /// </summary>
    /// <param name="target">
    ///     存储类型
    /// </param>
    protected abstract void GenerateFile(Type target);

    /// <summary>
    ///     获取文件名
    /// </summary>
    /// <param name="target">存储类型</param>
    /// <returns>文件名</returns>
    protected abstract string GetFilePath(Type target);

    /// <summary>
    ///     检查数据类型是否为 FileDataModelBase 的子类
    /// </summary>
    /// <param name="target">要检查的数据类型</param>
    /// <exception cref="InvalidDataModelException">如果数据类型不匹配</exception>
    protected static void CheckType(Type target)
    {
        if (!typeof(FileDataModelBase).IsAssignableFrom(target))
        {
            throw new InvalidDataModelException(target);
        }
    }

    /// <inheritdoc/>
    public abstract void Save<T>(IList<T> data, bool force = false) where T : FileDataModelBase;
    /// <inheritdoc/>
    public abstract void Add<T>(IList<T> data) where T : FileDataModelBase;
    /// <inheritdoc/>
    public abstract IList<T> Load<T>() where T : FileDataModelBase;
    /// <inheritdoc/>
    public abstract void Delete<T>(IList<T> target) where T : FileDataModelBase;
    /// <inheritdoc/>
    public abstract void Update<T>(IList<T> target) where T : FileDataModelBase;

    /// <inheritdoc/>
    public void Add<T>(params T[] data) where T : FileDataModelBase => Add(data.ToList());
    /// <inheritdoc/>
    public void Delete<T>(params T[] target) where T : FileDataModelBase => Delete(target.ToList());
    /// <inheritdoc/>
    public void Update<T>(params T[] data) where T : FileDataModelBase => Update(data.ToList());
}
