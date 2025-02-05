using System.Linq.Expressions;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     文件数据库管理器
/// </summary>
public abstract class FileDatabaseManagerBase : ManagerBase
{
    /// <summary>
    ///     初始化文件数据库管理器
    /// </summary>
    /// <param name="folderPath">
    ///     文件夹路径
    /// </param>
    /// <param name="id">
    ///     管理器 ID
    /// </param>
    protected FileDatabaseManagerBase(string folderPath, string id): base(id)
    {
        FolderPath = folderPath;
    }
    
    /// <summary>
    ///     锁对象，在多线程环境下使用
    /// </summary>
    protected object Locker = new();

    /// <summary>
    ///     文件夹路径
    /// </summary>
    public string FolderPath { get; set; }

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
    
    /// <inheritdoc/>
    public abstract override void Add<T>(IList<T> data);

    /// <inheritdoc/>
    public abstract override IList<T> Load<T>();

    /// <inheritdoc />
    public override IList<T> TryLoad<T>()
    {
        return Load<T>();
    }

    /// <inheritdoc/>
    public abstract override void Delete<T>(IList<T> target);

    /// <inheritdoc />
    public override void Clear<T>()
    {
        CheckType(typeof(T));
        Initialize<T>();
    }

    /// <inheritdoc/>
    public abstract override void Update<T>(IList<T> target);
    
    /// <inheritdoc />
    public abstract override List<T> Select<T>(Expression<Func<T, bool>> predicate);
    
}