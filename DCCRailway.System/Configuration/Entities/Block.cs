using System.Xml.Serialization;
using DCCRailway.System.Entities.Interfaces;

namespace DCCRailway.System.Config;

[XmlRoot(ElementName = "Block")]
public class Block : IBlock {
    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Description")]
    public string Description { get; set; }
}