using System.Linq.Expressions;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     数据库管理器基类
/// </summary>
public abstract class DatabaseManagerBase : IManager
{
    /// <summary>
    ///     连接字符串
    /// </summary>
    protected string? ConnectionString { get; set; }

    /// <inheritdoc />
    public void Save<T>(IList<T> data, bool force = false) where T : class, IData
    {
        if (force)
        {
            Clear<T>();
            Add(data);
        }
        else
        {
            var list = Load<T>();
            foreach (var item in data)
                if (!list.Contains(item))
                    Add(item);
                else
                    Update(item);
        }
    }

    /// <inheritdoc />
    public abstract void Add<T>(IList<T> data) where T : class, IData;

    /// <inheritdoc />
    public void Add<T>(params T[] data) where T : class, IData => Add(data.ToList());

    /// <inheritdoc />
    public abstract IList<T> Load<T>() where T : class, IData;

    /// <inheritdoc />
    public abstract void Delete<T>(IList<T> target) where T : class, IData;


    /// <inheritdoc />
    public abstract void Clear<T>() where T : class, IData;

    /// <inheritdoc />
    public void Delete<T>(params T[] target) where T : class, IData => Delete(target.ToList());

    /// <inheritdoc />
    public abstract void Update<T>(IList<T> data) where T : class, IData;

    /// <inheritdoc />
    public void Update<T>(params T[] data) where T : class, IData => Update(data.ToList());

    /// <inheritdoc />
    public abstract List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : class, IData;

    /// <summary>
    ///     根据 IData 创建一个表在指定数据库中
    /// </summary>
    /// <param name="data">
    ///     数据类型
    /// </param>
    /// <typeparam name="T">
    ///     数据类型
    /// </typeparam>
    public abstract void CreateTable<T>(T data) where T : class, IData;
}