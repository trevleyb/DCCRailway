using DCCRailway.Configuration.Base;
using DCCRailway.System.Types;

namespace DCCRailway.Configuration;
public class Decoder : ConfigBase {
    public Decoder() : base () {
        Manufacturer = new Manufacturer ("Unknown", 00);
    }

    public Manufacturer Manufacturer { get; set; }
    public string Model { get; set; }
    public string Family { get; set; }
    public DCCAddress Address { get; set; }
    public DCCAddressType AddressType { get; set; }
    public DCCProtocol Protocol { get; set; }
}