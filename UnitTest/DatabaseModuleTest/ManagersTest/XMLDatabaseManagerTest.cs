using A6ToolKits.Database.Exceptions;
using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public class XMLDatabaseManagerTest
{
    private readonly XMLDatabaseManager _manager = new XMLDatabaseManager("test");

    private static readonly List<TestXMLModel> data =
    [
        new() { Id = 1, Name = "A", Age = 1 },
        new() { Id = 2, Name = "B", Age = 2 },
        new() { Id = 3, Name = "C", Age = 3 }
    ];

    private void Initial()
    {
        var dataInFile = _manager.Load<TestXMLModel>();
        foreach (var item in dataInFile)
        {
            _manager.Delete(item);
        }
        _manager.Add(data);
    }
    
    [SetUp]
    public void Setup()
    {
        Initial();
    }
    
    [Test]
    public void LoadTest()
    {
        Initial();
        var result = _manager.Load<TestXMLModel>();
        Assert.That(result, Is.EqualTo(data));
    }

    [Test]
    [TestCase(1, "A", 1, false)]
    [TestCase(4, "D", 4, true)]
    public void TestAdd(int id, string name, int age, bool finished)
    {
        var testItem = new TestXMLModel { Id = id, Name = name, Age = age };
        var result = false;
        try
        {
            _manager.Add(testItem);
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
        var testItem = new TestXMLModel { Id = id, Name = name, Age = age };
        var result = false;
        try
        {
            _manager.Delete(testItem);
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
        var testItem = new TestXMLModel { Id = id, Name = name, Age = age };
        var result = false;
        try
        {
            _manager.Update(testItem);
            var dataInFile = _manager.Load<TestXMLModel>().FirstOrDefault(i => i.Id == id);
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
}