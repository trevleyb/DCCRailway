using System.Xml.Serialization;

namespace DCCRailway.System.Configuration.Entities;

[XmlRoot(ElementName = "Block")]
public class Block : IBlock {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Description")]
    public string Description { get; set; }
}