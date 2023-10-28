using System.Xml.Serialization;
using DCCRailway.System.Types;

namespace DCCRailway.System.Config;

[XmlRoot(ElementName = "Decoder")]
public class Decoder {
    public Decoder() {
        Parameters   = new Parameters();
        Manufacturer = new Manufacturer { Name = "Unknown", Identifier = 00 };
    }

    [XmlAttribute(AttributeName = "Manufacturer")]
    public Manufacturer Manufacturer { get; set; }

    [XmlAttribute(AttributeName = "Model")]
    public string Model { get; set; }

    [XmlAttribute(AttributeName = "Family")]
    public string Family { get; set; }

    [XmlAttribute(AttributeName = "Address")]
    public long Address { get; set; }

    [XmlAttribute(AttributeName = "AddressType")]
    public DCCAddressType AddressType { get; set; }

    [XmlAttribute(AttributeName = "SpeedSteps")]
    public DCCProtocol Protocol { get; set; }

    [XmlArray(ElementName = "Parameters")]
    public Parameters Parameters { get; set; }
}