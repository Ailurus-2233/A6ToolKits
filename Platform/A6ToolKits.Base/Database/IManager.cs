using System.Linq.Expressions;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database;

/// <summary>
///     数据库管理器
/// </summary>
public interface IManager
{
    /// <summary>
    ///     将数据保存到数据库，如果数据已经存在，
    ///     则更新数据，否则插入数据
    /// </summary>
    /// <param name="force">
    ///     是否强制保存
    /// </param>
    /// <param name="data">
    ///     数据列表
    /// </param>
    void Save<T>(IList<T> data, bool force = false) where T : class, IData;

    /// <summary>
    ///     增加数据，如果数据已经存在，则抛出异常
    /// </summary>
    /// <param name="data">
    ///     数据列表
    /// </param>
    void Add<T>(IList<T> data) where T : class, IData;

    /// <summary>
    ///     增加数据，如果数据已经存在，则抛出异常
    /// </summary>
    /// <param name="data">
    ///     数据列表
    /// </param>
    void Add<T>(params T[] data) where T : class, IData;

    /// <summary>
    ///     从数据库中加载数据
    /// </summary>
    /// <returns>
    ///     加载的数据列表
    /// </returns>
    IList<T> Load<T>() where T : class, IData;


    /// <summary>
    ///    从数据库中删除数据，如果数据不存在则抛出异常
    /// </summary>
    /// <param name="target">
    ///     删除的目标列表
    /// </param>
    void Delete<T>(IList<T> target) where T : class, IData;

    /// <summary>
    ///    从数据库中删除数据，如果数据不存在则抛出异常
    /// </summary>
    /// <param name="target">
    ///     删除的目标列表
    /// </param>
    void Delete<T>(params T[] target) where T : class, IData;
    
    /// <summary>
    ///     清空数据库中的数据
    /// </summary>
    /// <typeparam name="T">
    ///     数据类型
    /// </typeparam>
    void Clear<T>() where T : class, IData;

    /// <summary>
    ///     更新数据库中的数据，如果数据不存在则抛出异常
    /// </summary>
    /// <param name="data">
    ///     数据列表
    /// </param>
    void Update<T>(IList<T> data) where T : class, IData;

    /// <summary>
    ///     更新数据库中的数据，如果数据不存在则抛出异常
    /// </summary>
    /// <param name="data">
    ///     数据列表
    /// </param>
    void Update<T>(params T[] data) where T : class, IData;
    
    /// <summary>
    ///     从数据库中搜索满足条件的数据
    /// </summary>
    /// <param name="predicate">
    ///     搜索表达式
    /// </param>
    /// <returns>
    ///     搜索结果
    /// </returns>
    List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : class, IData;
}
