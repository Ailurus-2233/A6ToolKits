using A6ToolKits.Database.Exceptions;
using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public abstract class FileDatabaseManagerTestBase
{
    protected abstract FileDatabaseManagerBase DatabaseManager { get; }

    protected readonly List<TestModel> Data =
    [
        new() { Id = 1, Name = "A", Age = 1 },
        new() { Id = 2, Name = "B", Age = 2 },
        new() { Id = 3, Name = "C", Age = 3 }
    ];

    protected void Initialize()
    {
        var dataInFile = DatabaseManager.Load<TestModel>();
        foreach (var item in dataInFile)
        {
            DatabaseManager.Delete(item);
        }

        DatabaseManager.Add(Data);
    }

    [SetUp]
    public void Setup()
    {
        Initialize();
    }

    [Test]
    public void LoadTest()
    {
        Initialize();
        var result = DatabaseManager.Load<TestModel>();
        Assert.That(result, Is.EqualTo(Data));
    }

    [Test]
    [TestCase(1, "A", 1, false)]
    [TestCase(4, "D", 4, true)]
    public void TestAdd(int id, string name, int age, bool finished)
    {
        var testItem = new TestModel { Id = id, Name = name, Age = age };
        var result = false;
        try
        {
            DatabaseManager.Add(testItem);
            result = true;
        }
        catch (DataDuplicatedException)
        {
            result = false;
        }
        catch (Exception e)
        {
            Assert.Fail($"出现未知异常：{e.Message}");
        }

        Assert.That(result, Is.EqualTo(finished));
    }

    [Test]
    [TestCase(1, "A", 1, true)]
    [TestCase(100, "D", 4, false)]
    public void TestDelete(int id, string name, int age, bool finished)
    {
        var testItem = new TestModel { Id = id, Name = name, Age = age };
        var result = false;
        try
        {
            DatabaseManager.Delete(testItem);
            result = true;
        }
        catch (DataNotFoundException)
        {
            result = false;
        }
        catch (Exception e)
        {
            Assert.Fail($"出现未知异常：{e.Message}");
        }

        Assert.That(result, Is.EqualTo(finished));
    }

    [Test]
    [TestCase(1, "B", 100, true)]
    [TestCase(100, "D", 4, false)]
    public void TestUpdate(int id, string name, int age, bool finished)
    {
        var testItem = new TestModel { Id = id, Name = name, Age = age };
        var result = false;
        try
        {
            DatabaseManager.Update(testItem);
            var dataInFile = DatabaseManager.Load<TestModel>().FirstOrDefault(i => i.Id == id);
            result = dataInFile?.Name == name && dataInFile.Age == age;
        }
        catch (DataNotFoundException)
        {
            result = false;
        }
        catch (Exception e)
        {
            Assert.Fail($"出现未知异常：{e.Message}");
        }

        Assert.That(result, Is.EqualTo(finished));
    }

    protected static Func<TestModel, bool>[] QueryCases =
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
    public void TestSelect(Func<TestModel, bool> queryCase)
    {
        Initialize();
        var result = DatabaseManager.Select(queryCase);
        var expected = Data.Where(queryCase).ToList();
        Assert.That(result, Is.EqualTo(expected));
    }
}