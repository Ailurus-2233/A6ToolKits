using System.Reflection;
using System.Xml;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataConverters;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     XML 数据库管理器
/// </summary>
public class XMLDatabaseManager(string path) : FileDatabaseManagerBase
{
    /// <inheritdoc/>
    protected override string FolderPath { get; } = path;

    /// <inheritdoc/>
    public override void Add<T>(IList<T> data)
    {
        var (xml, root) = LoadDocument<T>();
        var datasetIndexList = LoadIndex<T>();


        foreach (var item in data)
        {
            if (datasetIndexList.Contains(item.GetHashCode()))
            {
                throw new DataDuplicatedException($"{item} is already exist in dataset");
            }

            if (item is not IData dataModel) continue;
            var element = dataModel.ToXml();
            _ = root.AppendChild(xml.ImportNode(element, true));
        }

        xml.Save(GetFilePath(typeof(T)));
    }

    /// <inheritdoc/>
    public override void Delete<T>(IList<T> target)
    {
        var (xml, root) = LoadDocument<T>();
        var datasetIndexList = LoadIndex<T>();

        foreach (var item in target)
        {
            var index = datasetIndexList.IndexOf(item.GetHashCode());
            if (index == -1)
                throw new DataNotFoundException($"{item} is not exist in dataset");

            var nodeToRemove = root.ChildNodes[index];
            if (nodeToRemove != null)
                _ = root.RemoveChild(nodeToRemove);
        }

        xml.Save(GetFilePath(typeof(T)));
    }

    /// <inheritdoc/>
    public override IList<T> Load<T>()
    {
        var (_, root) = LoadDocument<T>();
        List<T> dataset = [];

        foreach (XmlElement element in root.ChildNodes)
        {
            if (Activator.CreateInstance(typeof(T)) is not IData dataModel)
                throw new InvalidDataModelException(typeof(T));
            dataModel.FromXml(element);
            dataset.Add((T)dataModel);
        }

        return dataset;
    }

    /// <inheritdoc/>
    public override void Update<T>(IList<T> target)
    {
        var (xml, root) = LoadDocument<T>();
        var datasetIndexList = LoadIndex<T>();

        foreach (var item in target)
        {
            var index = datasetIndexList.IndexOf(item.GetHashCode());
            if (index == -1)
                throw new DataNotFoundException($"{item} is not exist in dataset");

            if (item is not IData dataModel) continue;
            var element = dataModel.ToXml();
            var oldChild = root.ChildNodes[index];
            if (oldChild != null)
                _ = root.ReplaceChild(xml.ImportNode(element, true), oldChild);
        }

        xml.Save(GetFilePath(typeof(T)));
    }

    /// <inheritdoc />
    public override List<T> Select<T>(Func<T, bool> query)
    {
        var (_, root) = LoadDocument<T>();
        List<T> result = [];

        foreach (XmlElement element in root.ChildNodes)
        {
            if (Activator.CreateInstance(typeof(T)) is not IData dataModel)
                throw new InvalidDataModelException(typeof(T));
            dataModel.FromXml(element);
            if (query((T)dataModel))
                result.Add((T)dataModel);
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
        
        var targetName = targetType.Name;
        XmlDocument xml = new();
        var declaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
        _ = xml.AppendChild(declaration);
        var root = xml.CreateElement(targetName);
        _ = xml.AppendChild(root);

        var filePath = GetFilePath(targetType);
        xml.Save(filePath);
    }
    

    /// <inheritdoc/>
    protected override string GetFilePath(Type target)
    {
        CheckType(target);
        var instance = Activator.CreateInstance(target) as IData;
        var tableName = instance?.GetTableName();
        return Path.Combine(FolderPath, $"{tableName}.xml");
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
        var filePath = GetFilePath(type);

        if (!File.Exists(filePath))
            Initialize<T>();

        xml.Load(GetFilePath(type));

        var root = xml.DocumentElement ?? throw new DataNotExistException(GetFilePath(type));
        return (xml, root);
    }
}