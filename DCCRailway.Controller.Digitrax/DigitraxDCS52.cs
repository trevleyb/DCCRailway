using DCCRailway.Common.Types;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using Serilog;

namespace DCCRailway.Controller.Digitrax;

[Controller("DCS52", "DigiTrax", "DCS52")]
public class DCS52(ILogger logger) : CommandStation(logger), ICommandStation {
    public override DCCAddress CreateAddress()                                                       => new();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new(address, type);
}