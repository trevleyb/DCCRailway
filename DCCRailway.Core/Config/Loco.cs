using System.Xml.Serialization;

namespace DCCRailway.Core.Config;

[XmlRoot(ElementName = "Loco")]
public class Loco {
    public Loco() {
        Parameters = new Parameters();

        Decoder = new Decoder();
    }

    [XmlAttribute(AttributeName = "Id")] public string Id { get; set; }

    [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }

    [XmlAttribute(AttributeName = "Description")]
    public string Description { get; set; }

    [XmlAttribute(AttributeName = "Type")] public string Type { get; set; }

    [XmlAttribute(AttributeName = "RoadName")]
    public string RoadName { get; set; }

    [XmlAttribute(AttributeName = "RoadNumber")]
    public string RoadNumber { get; set; }

    [XmlAttribute(AttributeName = "Manufacturer")]
    public string Manufacturer { get; set; }

    [XmlAttribute(AttributeName = "Model")]
    public string Model { get; set; }

    [XmlElement(ElementName = "Decoder")] public Decoder Decoder { get; set; }

    [XmlArray(ElementName = "Parameters")] public Parameters Parameters { get; set; }

    public new string ToString() {
        return $"{Id}:{Name}";
    }
}