using System.Xml;

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
        (XmlDocument? xml, XmlElement? root) = LoadDocument<T>();
        IList<int> datasetIndexList = LoadIndex<T>();


        foreach (T item in data)
        {
            if (datasetIndexList.Contains(item.GetHashCode()))
            {
                throw new DataDuplicatedException($"{item} is already exist in dataset");
            }

            if (item is FileDataModelBase fileDataModel)
            {
                XmlElement element = fileDataModel.ToXml();
                _ = root.AppendChild(xml.ImportNode(element, true));
            }
        }

        string targetPath = GetFileName(typeof(T));
        xml.Save(targetPath);
    }

    /// <inheritdoc/>
    public override void Delete<T>(IList<T> target)
    {
        CheckType(typeof(T));
        (XmlDocument? xml, XmlElement? root) = LoadDocument<T>();
        IList<int> datasetIndexList = LoadIndex<T>();

        foreach (T item in target)
        {
            int index = datasetIndexList.IndexOf(item.GetHashCode());
            if (index == -1)
            {
                throw new DataNotFoundException($"{item} is not exist in dataset");
            }

            XmlNode? nodeToRemove = root.ChildNodes[index];
            if (nodeToRemove != null)
            {
                _ = root.RemoveChild(nodeToRemove);
            }
        }

        string targetPath = GetFileName(typeof(T));
        xml.Save(targetPath);
    }

    /// <inheritdoc/>
    public override IList<T> Load<T>()
    {
        CheckType(typeof(T));
        (_, XmlElement? root) = LoadDocument<T>();
        List<T> dataset = new();

        foreach (XmlElement element in root.ChildNodes)
        {
            if (Activator.CreateInstance(typeof(T)) is not FileDataModelBase fileDataModel)
            {
                throw new InvalidDataModelException(typeof(T));
            }

            fileDataModel.FromXml(element);
            dataset.Add((T)(object)fileDataModel);
        }
        return dataset;
    }

    /// <inheritdoc/>
    public override void Save<T>(IList<T> data, bool force = false)
    {
        CheckType(typeof(T));
        (XmlDocument? xml, XmlElement? root) = LoadDocument<T>();
        IList<T> dataset = Load<T>();
        IList<int> datasetIndexList = LoadIndex<T>();

        if (force)
        {
            root.RemoveAll();
            datasetIndexList.Clear();
            Add(data);
        }
        else
        {
            List<T> updateList = new();
            List<T> addList = new();
            List<T> deleteList = new();
            foreach (T item in data)
            {
                if (datasetIndexList.Contains(item.GetHashCode()))
                {
                    updateList.Add(item);
                }
                else
                {
                    addList.Add(item);
                }
            }

            List<int> dataIndexList = data.Select(item => item.GetHashCode()).ToList();
            foreach (int index in datasetIndexList)
            {
                if (!dataIndexList.Contains(index))
                {
                    T? target = dataset.FirstOrDefault(item => item.GetHashCode() == index);
                    if (target != null)
                    {
                        deleteList.Add(target);
                    }
                }
            }

            Add(addList);
            Delete(deleteList);
            Update(updateList);
        }
    }

    /// <inheritdoc/>
    public override void Update<T>(IList<T> target)
    {
        CheckType(typeof(T));
        (XmlDocument? xml, XmlElement? root) = LoadDocument<T>();
        IList<int> datasetIndexList = LoadIndex<T>();

        foreach (T item in target)
        {
            int index = datasetIndexList.IndexOf(item.GetHashCode());
            if (index == -1)
            {
                throw new DataNotFoundException($"{item} is not exist in dataset");
            }

            if (item is FileDataModelBase fileDataModel)
            {
                XmlElement element = fileDataModel.ToXml();
                XmlNode? oldChild = root.ChildNodes[index];
                if (oldChild != null)
                {
                    _ = root.ReplaceChild(xml.ImportNode(element, true), oldChild);
                }
            }
        }
    }

    /// <inheritdoc/>
    protected override void GenerateFile(Type target)
    {
        string targetName = target.Name;
        XmlDocument xml = new();
        XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
        _ = xml.AppendChild(declaration);
        XmlElement root = xml.CreateElement(targetName);
        _ = xml.AppendChild(root);
        xml.Save(GetFileName(target));
    }

    /// <inheritdoc/>
    protected override string GetFileName(Type target)
    {
        return Path.Combine(FolderPath, $"{target.Name}.xml");
    }
}