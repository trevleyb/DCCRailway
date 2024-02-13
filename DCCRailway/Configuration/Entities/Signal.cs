using System.Xml.Serialization;
using DCCRailway.System.Types;

namespace DCCRailway.Configuration.Entities;

[XmlRoot(ElementName = "Signal")]
public class Signal {
    public Signal() {
        Parameters = new Parameters();
        Decoder    = new Decoder { AddressType = DCCAddressType.Signal };
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