using System.Linq.Expressions;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database.Managers;

/// <inheritdoc />
public abstract class ManagerBase : IManager
{

    /// <summary>
    ///     初始化管理器
    /// </summary>
    /// <param name="id">
    ///     管理器 ID
    /// </param>
    protected ManagerBase(string id)
    {
        ManagerId = id;
    }
    
    /// <summary>
    ///     管理器 ID
    /// </summary>
    public string ManagerId { get; set; }

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

    #region IManager

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public void Save<T>(IList<T> data, bool force = false) where T : class, IData
    {
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
            deleteList.AddRange(from index in datasetIndexList
                where !dataIndexList.Contains(index)
                select dataset.FirstOrDefault(item => item.GetHashCode() == index));

            Add(addList);
            Delete(deleteList);
            Update(updateList);
        }
    }

    public void Save<T>(params T[] data) where T : class, IData => Save(data.ToList());
    public abstract void Add<T>(IList<T> data) where T : class, IData;
    public void Add<T>(params T[] data) where T : class, IData => Add(data.ToList());
    public abstract IList<T> Load<T>() where T : class, IData;
    public abstract IList<T> TryLoad<T>() where T : class, IData;
    public abstract void Delete<T>(IList<T> target) where T : class, IData;
    public void Delete<T>(params T[] target) where T : class, IData => Delete(target.ToList());
    public abstract void Clear<T>() where T : class, IData;
    public abstract void Update<T>(IList<T> data) where T : class, IData;
    public void Update<T>(params T[] data) where T : class, IData => Update(data.ToList());
    public abstract List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : class, IData;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

    #endregion
}