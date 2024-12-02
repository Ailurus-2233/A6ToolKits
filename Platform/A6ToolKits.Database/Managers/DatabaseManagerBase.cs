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
    public abstract void Save<T>(IList<T> data, bool force = false) where T : IData;

    /// <inheritdoc />
    public abstract void Add<T>(IList<T> data) where T : IData;

    /// <inheritdoc />
    public abstract void Add<T>(params T[] data) where T : IData;

    /// <inheritdoc />
    public abstract IList<T> Load<T>() where T : IData;

    /// <inheritdoc />
    public abstract void Delete<T>(IList<T> target) where T : IData;

    /// <inheritdoc />
    public abstract void Delete<T>(params T[] target) where T : IData;

    /// <inheritdoc />
    public abstract void Update<T>(IList<T> data) where T : IData;

    /// <inheritdoc />
    public abstract void Update<T>(params T[] data) where T : IData;
}