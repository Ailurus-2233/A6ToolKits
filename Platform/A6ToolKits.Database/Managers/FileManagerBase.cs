using A6ToolKits.Database.DataModel;
using A6ToolKits.Database.Exceptions;
using System.Xml;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     数据库管理器
/// </summary>
public abstract class FileManagerBase : IManager
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
    protected IList<int> LoadIndex<T>() where T : IData
    {
        IList<T> dataset = Load<T>();
        List<int> result = new();
        foreach (T item in dataset)
        {
            result.Add(item.GetHashCode());
        }
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
    protected abstract string GetFileName(Type target);

    /// <summary>
    ///     检查数据类型是否为 FileDataModelBase 的子类
    /// </summary>
    /// <param name="target">要检查的数据类型</param>
    /// <exception cref="InvalidDataModelException">如果数据类型不匹配</exception>
    protected static void CheckType(Type target)
    {
        if (!target.IsAssignableFrom(typeof(FileDataModelBase)))
        {
            throw new InvalidDataModelException(target);
        }
    }

    /// <summary>
    ///     加载 XML 文档
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <returns>XML 文档</returns>
    /// <exception cref="InvalidDataModelException">如果数据类型不匹配</exception>
    protected (XmlDocument, XmlElement) LoadDocument<T>() where T : IData
    {
        CheckType(typeof(T));
        var type = typeof(T);
        XmlDocument xml = new();
        xml.Load(GetFileName(type));
        var root = xml.DocumentElement ?? throw new DataNotExistException(GetFileName(type));
        return (xml, root);
    }

    /// <inheritdoc/>
    public abstract void Save<T>(IList<T> data, bool force = false) where T : IData;
    /// <inheritdoc/>
    public abstract void Add<T>(IList<T> data) where T : IData;
    /// <inheritdoc/>
    public abstract IList<T> Load<T>() where T : IData;
    /// <inheritdoc/>
    public abstract void Delete<T>(IList<T> target) where T : IData;
    /// <inheritdoc/>
    public abstract void Update<T>(IList<T> target) where T : IData;

    /// <inheritdoc/>
    public void Add<T>(params T[] data) where T : IData => Add(data);
    /// <inheritdoc/>
    public void Delete<T>(params T[] target) where T : IData => Delete(target);
    /// <inheritdoc/>
    public void Update<T>(params T[] data) where T : IData => Update(data);
}
