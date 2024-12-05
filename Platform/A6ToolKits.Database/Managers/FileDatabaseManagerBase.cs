using System.Linq.Expressions;
using A6ToolKits.Database.Exceptions;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     文件数据库管理器
/// </summary>
public abstract class FileDatabaseManagerBase : IManager
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
    protected IList<int> LoadIndex<T>() where T : class, IData
    {
        var dataset = Load<T>();
        List<int> result = [];
        result.AddRange(dataset.Select(item => item.GetHashCode()));
        return result;
    }

    /// <summary>
    ///     初始化数据存储文件
    /// </summary>
    protected abstract void Initialize<T>() where T : IData;

    /// <summary>
    ///     获取文件名
    /// </summary>
    /// <param name="target">存储类型</param>
    /// <returns>文件名</returns>
    protected abstract string GetFilePath(Type target);

    /// <summary>
    ///     检查数据类型是否为 DataModelBase 的子类
    /// </summary>
    /// <param name="target">
    ///     要检查的数据类型
    /// </param>
    /// <returns>
    ///     true 如果数据类型匹配
    /// </returns>
    /// <exception cref="InvalidDataModelException">
    ///     抛出异常，如果数据类型不匹配
    /// </exception>
    protected static bool CheckType(Type target)
    {
        if (!typeof(DataModelBase).IsAssignableFrom(target))
            throw new InvalidDataModelException(target);
        return true;
    }

    /// <inheritdoc/>
    public void Save<T>(IList<T> data, bool force = false) where T : class, IData
    {
        CheckType(typeof(T));

        if (force)
        {
            Clear<T>();
            Add(data);
        }
        else
        {
            var dataset = Load<T>();
            var datasetIndexList = LoadIndex<T>();

            List<T> updateList = [];
            List<T> addList = [];
            List<T> deleteList = [];
            foreach (var item in data)
            {
                if (datasetIndexList.Contains(item.GetHashCode()))
                    updateList.Add(item);
                else
                    addList.Add(item);
            }

            var dataIndexList = data.Select(item => item.GetHashCode()).ToList();
            foreach (var index in datasetIndexList)
            {
                if (dataIndexList.Contains(index)) continue;
                var target = dataset.FirstOrDefault(item => item.GetHashCode() == index);
                if (target != null)
                    deleteList.Add(target);
            }

            Add(addList);
            Delete(deleteList);
            Update(updateList);
        }
    }

    /// <inheritdoc/>
    public abstract void Add<T>(IList<T> data) where T : class, IData;

    /// <inheritdoc/>
    public abstract IList<T> Load<T>() where T : class, IData;

    /// <inheritdoc/>
    public abstract void Delete<T>(IList<T> target) where T : class, IData;

    /// <inheritdoc />
    public void Clear<T>() where T : class, IData
    {
        CheckType(typeof(T));
        Initialize<T>();
    }

    /// <inheritdoc/>
    public abstract void Update<T>(IList<T> target) where T : class, IData;

    /// <inheritdoc/>
    public void Add<T>(params T[] data) where T : class, IData => Add(data.ToList());

    /// <inheritdoc/>
    public void Delete<T>(params T[] target) where T : class, IData => Delete(target.ToList());

    /// <inheritdoc/>
    public void Update<T>(params T[] data) where T : class, IData => Update(data.ToList());

    /// <inheritdoc />
    public abstract List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : class, IData;
    
}