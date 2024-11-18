using System.Xml;

namespace A6ToolKits.Database;

public interface IData
{   
    XmlElement ToXml();
    void FromXml(XmlElement xml);
    
    string ToJson();
    void FromJson(string json);
    
    string GetCSVHeader();
    string ToCSVLine();
    void FromCSVLine(string csvLine);
}