using System.Linq.Expressions;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     数据库管理器基类
/// </summary>
public abstract class DatabaseManagerBase(string id) : ManagerBase(id)
{
    /// <summary>
    ///     连接字符串
    /// </summary>
    protected string? ConnectionString { get; set; }
    
    #region ManagerBase

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

    #endregion

    /// <inheritdoc />
    public abstract override void Add<T>(IList<T> data);

    /// <inheritdoc />
    public abstract override IList<T> Load<T>();

    /// <inheritdoc />
    public abstract override void Delete<T>(IList<T> target);


    /// <inheritdoc />
    public abstract override void Clear<T>();

    /// <inheritdoc />
    public abstract override void Update<T>(IList<T> data);

    /// <inheritdoc />
    public abstract override List<T> Select<T>(Expression<Func<T, bool>> predicate);

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