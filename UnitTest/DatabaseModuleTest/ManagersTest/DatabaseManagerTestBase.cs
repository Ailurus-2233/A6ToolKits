using System.Linq.Expressions;
using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public abstract class DatabaseManagerTestBase
{
    protected abstract DatabaseManagerBase? DatabaseManager { get; }
    
    protected readonly List<SQLTestModel> Data =
    [
        new() { Id = 1, Name = "A", Age = 1 },
        new() { Id = 2, Name = "B", Age = 2 },
        new() { Id = 3, Name = "C", Age = 3 }
    ];
    
    public abstract void Initialize();

    [SetUp]
    public void SetUp()
    {
        Initialize();
    }
    
    [Test]
    public void CreateTableTest()
    {
        DatabaseManager.CreateTable<SQLTestModel>();
    }

    [Test]
    public void AddTest()
    {
        DatabaseManager.Add(Data);
        var result = DatabaseManager.Load<SQLTestModel>();
        Assert.That(result, Is.EqualTo(Data));
    }

    [Test]
    public void LoadTest()
    {
        DatabaseManager.Clear<SQLTestModel>();
        var result = DatabaseManager.Load<SQLTestModel>();
        Assert.That(result, Is.Empty);
        
        DatabaseManager.Add(Data);
        result = DatabaseManager.Load<SQLTestModel>();
        Assert.That(result, Is.EqualTo(Data));
    }
    
    [Test]
    public void DeleteTest()
    {
        DatabaseManager.Add(Data);
        DatabaseManager.Delete(Data[0]);
        var result = DatabaseManager.Load<SQLTestModel>();
        Assert.That(result, Has.Count.EqualTo(Data.Count - 1));
    }

    protected static Expression<Func<SQLTestModel, bool>>[] QueryCases =
    [
        i => i.Id == 1,
        i => i.Name == "A",
        i => i.Age == 1,
        i => i.Age > 1,
        i => i.Age != 2,
        i => i.Id > 1 && i.Name != "B"
    ];
    
    [Test]
    [TestCaseSource(nameof(QueryCases))]
    public void SelectTest(Expression<Func<SQLTestModel, bool>> expression)
    {
        DatabaseManager.Clear<SQLTestModel>();
        DatabaseManager.Add(Data);
        var result = DatabaseManager.Select(expression);
        var target = Data.Where(expression.Compile()).ToList();
        Assert.That(result, Is.EqualTo(target));
    }
}