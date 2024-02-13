using System.Xml.Serialization;
using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Configuration.Entities;

[XmlRoot(ElementName = "Turnout")]
public class Turnout {
    public Turnout() {
        Parameters = new Parameters();
        Decoder    = new Decoder { AddressType = DCCAddressType.Turnout };
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "Decoder")]
    public Decoder Decoder { get; set; }

    [XmlArray(ElementName = "Parameters")]
    public Parameters Parameters { get; set; }
}