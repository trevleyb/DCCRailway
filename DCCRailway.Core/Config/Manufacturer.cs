using System.Xml.Serialization;

namespace DCCRailway.Core.Config;

public class Manufacturer {
    [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }

    [XmlAttribute(AttributeName = "ID")] public byte Identifier { get; set; }
}