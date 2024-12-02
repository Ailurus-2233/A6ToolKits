using A6ToolKits.Database;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.Enums;

namespace DatabaseModuleTest.DataModels;

[TableName(nameof(TestXMLModel))]
public class TestXMLModel : FileDataModelBase
{
    [PrimaryKey]
    [ColumnType("Id", ColumnType.XML_INTEGER)]
    public int Id { get; set; }
    
    [ColumnType("name", ColumnType.XML_STRING)]
    public string Name { get; set; }
    
    [ColumnType("age", ColumnType.XML_INTEGER)]
    public int Age { get; set; }
}