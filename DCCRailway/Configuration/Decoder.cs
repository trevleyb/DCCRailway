using System.Xml.Serialization;
using DCCRailway.System.Types;

namespace DCCRailway.Configuration;
public class Decoder {
    public Decoder() {
        Parameters   = new Parameters();
        Manufacturer = new Manufacturer { Name = "Unknown", Identifier = 00 };
    }

    public Manufacturer Manufacturer { get; set; }
    public string Model { get; set; }
    public string Family { get; set; }
    public long Address { get; set; }
    public DCCAddressType AddressType { get; set; }
    public DCCProtocol Protocol { get; set; }
    public Parameters Parameters { get; set; }
}