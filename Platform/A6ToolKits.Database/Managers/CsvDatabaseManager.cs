using System.Linq.Expressions;
using System.Text;
using A6ToolKits.Database.DataConverters;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     CSV 数据库管理器
/// </summary>
/// <param name="path">
///     数据库文件夹路径
/// </param>
public class CsvDatabaseManager(string folderPath, string id) : FileDatabaseManagerBase(folderPath, id)
{
    private const char Split = ',';

    /// <inheritdoc />
    protected override string GetFilePath(Type target)
    {
        CheckType(target);
        var instance = Activator.CreateInstance(target) as IData;
        var tableName = instance?.GetTableName();
        return Path.Combine(FolderPath, $"{tableName}.csv");
    }

    /// <inheritdoc />
    public override void Add<T>(IList<T> data)
    {
        CheckType(typeof(T));
        var targetPath = GetFilePath(typeof(T));
        if (!File.Exists(targetPath))
            Initialize<T>();

        var datasetIndexList = LoadIndex<T>();

        using var file = File.Open(targetPath, FileMode.Append);
        foreach (var item in data)
        {
            if (datasetIndexList.Contains(item.GetHashCode()))
                throw new DataDuplicatedException($"{item} is already exist in dataset");

            if (item is not IData dataModel) continue;
            var line = dataModel.ToCsvLine(Split);
            var bytes = Encoding.UTF8.GetBytes(line);
            file.Write(bytes, 0, bytes.Length);
        }

        file.Close();
    }

    /// <inheritdoc />
    public override IList<T> Load<T>()
    {
        CheckType(typeof(T));
        var targetPath = GetFilePath(typeof(T));
        if (!File.Exists(targetPath))
            Initialize<T>();

        var result = new List<T>();
        var lines = File.ReadAllLines(targetPath);
        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (Activator.CreateInstance(typeof(T)) is not IData dataModel)
                throw new InvalidDataModelException(typeof(T));
            dataModel.FromCsvLine(line, Split);
            result.Add((T) dataModel);
        }

        return result;
    }

    /// <inheritdoc />
    public override void Delete<T>(IList<T> target)
    {
        CheckType(typeof(T));
        var targetPath = GetFilePath(typeof(T));
        if (!File.Exists(targetPath))
            throw new DataNotFoundException($"{typeof(T)} is not exist in database folder");

        var dataset = Load<T>();

        foreach (var deleteItem in target)
        {
            if (!dataset.Contains(deleteItem))
                throw new DataNotFoundException($"{deleteItem} is not exist in dataset");
        }
        
        using var file = File.Open(targetPath, FileMode.Truncate);

        var header = dataset[0].GetCsvHeader(Split);
        var bytes = Encoding.UTF8.GetBytes(header);
        file.Write(bytes, 0, bytes.Length);
        
        foreach (var data in dataset)
        {
            if (target.Contains(data))
                continue;
            var line = data.ToCsvLine(Split);
            bytes = Encoding.UTF8.GetBytes(line);
            file.Write(bytes, 0, bytes.Length);
        }
        
        file.Close();
    }

    /// <inheritdoc />
    public override void Update<T>(IList<T> target)
    {
        CheckType(typeof(T));
        var targetPath = GetFilePath(typeof(T));
        if (!File.Exists(targetPath))
            throw new DataNotFoundException($"{typeof(T)} is not exist in database folder");

        var dataset = Load<T>();

        foreach (var updateItem in target)
        {
            if (!dataset.Contains(updateItem))
                throw new DataNotFoundException($"{updateItem} is not exist in dataset");
        }
        
        using var file = File.Open(targetPath, FileMode.Truncate);
        
        var header = dataset[0].GetCsvHeader(Split);
        var bytes = Encoding.UTF8.GetBytes(header);
        file.Write(bytes, 0, bytes.Length);
        
        foreach (var data in dataset)
        {
            var index = target.IndexOf(data);
            if (index != -1)
            {
                var line = target[index].ToCsvLine(Split);
                bytes = Encoding.UTF8.GetBytes(line);
            }
            else
            {
                var line = data.ToCsvLine(Split);
                bytes = Encoding.UTF8.GetBytes(line);
            }

            file.Write(bytes, 0, bytes.Length);
        }
        
        file.Close();
    }

    /// <inheritdoc />
    public override List<T> Select<T>(Expression<Func<T, bool>> predicate)
    {
        CheckType(typeof(T));
        var targetPath = GetFilePath(typeof(T));
        if (!File.Exists(targetPath))
            Initialize<T>();

        var result = new List<T>();
        var lines = File.ReadAllLines(targetPath);
        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (Activator.CreateInstance(typeof(T)) is not IData dataModel)
                throw new InvalidDataModelException(typeof(T));
            dataModel.FromCsvLine(line, Split);
            if (predicate.Compile().Invoke((T) dataModel))
                result.Add((T) dataModel);
        }

        return result;
    }

    /// <inheritdoc />
    protected override void Initialize<T>()
    {
        var targetType = typeof(T);
        var targetPath = GetFilePath(targetType);
        if (File.Exists(targetPath))
            File.Delete(targetPath);
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);
        
        if (Activator.CreateInstance(typeof(T)) is not IData dataModel)
            throw new InvalidDataModelException(typeof(T));
        
        var header = dataModel.GetCsvHeader(Split);
        File.WriteAllText(targetPath, header);
    }
}