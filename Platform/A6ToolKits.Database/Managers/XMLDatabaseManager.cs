using System.Reflection;
using System.Xml;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataConverters;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     XML 数据库管理器
/// </summary>
public class XMLDatabaseManager(string path) : FileManagerBase
{
    /// <inheritdoc/>
    protected override string FolderPath { get; } = path;

    /// <inheritdoc/>
    public override void Add<T>(IList<T> data)
    {
        CheckType(typeof(T));
        var (xml, root) = LoadDocument<T>();
        var datasetIndexList = LoadIndex<T>();


        foreach (var item in data)
        {
            if (datasetIndexList.Contains(item.GetHashCode()))
            {
                throw new DataDuplicatedException($"{item} is already exist in dataset");
            }

            if (item is not FileDataModelBase fileDataModel) continue;
            var element = fileDataModel.ToXml();
            _ = root.AppendChild(xml.ImportNode(element, true));
        }

        xml.Save(GetFilePath(typeof(T)));
    }

    /// <inheritdoc/>
    public override void Delete<T>(IList<T> target)
    {
        CheckType(typeof(T));
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
        CheckType(typeof(T));
        var (_, root) = LoadDocument<T>();
        List<T> dataset = [];

        foreach (XmlElement element in root.ChildNodes)
        {
            if (Activator.CreateInstance(typeof(T)) is not FileDataModelBase fileDataModel)
                throw new InvalidDataModelException(typeof(T));
            fileDataModel.FromXml(element);
            dataset.Add((T)fileDataModel);
        }

        return dataset;
    }

    /// <inheritdoc/>
    public override void Save<T>(IList<T> data, bool force = false)
    {
        CheckType(typeof(T));
        var (xml, root) = LoadDocument<T>();
        var dataset = Load<T>();
        var datasetIndexList = LoadIndex<T>();

        if (force)
        {
            root.RemoveAll();
            datasetIndexList.Clear();
            Add(data);
        }
        else
        {
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
        
        xml.Save(GetFilePath(typeof(T)));
    }

    /// <inheritdoc/>
    public override void Update<T>(IList<T> target)
    {
        CheckType(typeof(T));
        var (xml, root) = LoadDocument<T>();
        var datasetIndexList = LoadIndex<T>();

        foreach (var item in target)
        {
            var index = datasetIndexList.IndexOf(item.GetHashCode());
            if (index == -1)
            {
                throw new DataNotFoundException($"{item} is not exist in dataset");
            }

            if (item is not FileDataModelBase fileDataModel) continue;
            var element = fileDataModel.ToXml();
            var oldChild = root.ChildNodes[index];
            if (oldChild != null)
                _ = root.ReplaceChild(xml.ImportNode(element, true), oldChild);
        }
        
        xml.Save(GetFilePath(typeof(T)));
    }

    /// <inheritdoc/>
    protected override void GenerateFile(Type target)
    {
        var targetName = target.Name;
        XmlDocument xml = new();
        var declaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
        _ = xml.AppendChild(declaration);
        var root = xml.CreateElement(targetName);
        _ = xml.AppendChild(root);
        
        var filePath = GetFilePath(target);
        xml.Save(filePath);
    }

    /// <inheritdoc/>
    protected override string GetFilePath(Type target)
    {
        var tableName = target.GetCustomAttribute<TableNameAttribute>()?.Name ?? target.Name;
        return Path.Combine(FolderPath, $"{tableName}.xml");
    }

    /// <summary>
    ///     加载 XML 文档
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <returns>XML 文档</returns>
    /// <exception cref="InvalidDataModelException">如果数据类型不匹配</exception>
    protected (XmlDocument, XmlElement) LoadDocument<T>() where T : FileDataModelBase
    {
        CheckType(typeof(T));
        var type = typeof(T);
        XmlDocument xml = new();
        var filePath = GetFilePath(type);
        if (!File.Exists(filePath))
        {
            if (!Directory.Exists(FolderPath))
                _ = Directory.CreateDirectory(FolderPath);
            File.Create(filePath).Close();
        }

        try
        {
            xml.Load(GetFilePath(type));
        }
        catch (XmlException e)
        {
            GenerateFile(type);
            xml.Load(GetFilePath(type));
        }
        var root = xml.DocumentElement ?? throw new DataNotExistException(GetFilePath(type));
        return (xml, root);
    }
}