namespace A6ToolKits.Database;

public abstract class FileManagerBase : IManager
{
    protected abstract string FolderPath { get; }
    
    public abstract void Save(IList<IData> data);
    public abstract IList<IData> Load(IData target);
    public abstract void Delete(IData target);
    public abstract void Update(IData target);
}