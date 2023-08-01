using System.Xml.Serialization;

namespace DCCRailway.Core.Config;

[XmlRoot(ElementName = "Block")]
public class Block {
    [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }

    [XmlAttribute(AttributeName = "Description")]
    public string Description { get; set; }
}