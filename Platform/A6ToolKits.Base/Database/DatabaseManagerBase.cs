using A6ToolKits.Database.Enums;

namespace A6ToolKits.Database;

public abstract class DatabaseManagerBase: IManager
{
    protected abstract string ConnectionString { get; }
    
    public abstract void Save(IList<IData> data);
    public abstract IList<IData> Load(IData target);
    public abstract void Delete(IData target);
    public abstract void Update(IData target);
}