using DCCRailway.Common.Types;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using Serilog;

namespace DCCRailway.Controller.Sprog;

[Controller("Sprog II", "DCCSystems", "Sprog II")]
public class Sprog2(ILogger logger) : CommandStation(logger), ICommandStation {
    public override DCCAddress CreateAddress()                                                       => new();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new(address, type);
}