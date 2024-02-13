using DCCRailway.System.Controllers;
using DCCRailway.System.Types;

namespace DCCRailway.Manufacturer.Digitrax;

[Controller("DCS52", "DigiTrax", "DCS52")]
public class DCS52 : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<DigitraxVirtualAdapter>();
    }

    protected override void RegisterCommands() {
        //RegisterCommand<IDummyCmd> (typeof (Commands.VirtualDummy));
        //RegisterCommand<ICmdStatus> (typeof (Commands.VirtualStatus));
    }
}