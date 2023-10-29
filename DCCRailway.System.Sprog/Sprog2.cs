using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;
using DCCRailway.System.Virtual;

namespace DCCRailway.System.Sprog;

[System("Sprog II", "DCCSystems", "Sprog II")]
public class Sprog2 : System, ISystem {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<SprogVirtualAdapter>();
    }

    protected override void RegisterCommands() {
        //RegisterCommand<IDummyCmd> (typeof (Commands.VirtualDummy));
        //RegisterCommand<ICmdStatus> (typeof (Commands.VirtualStatus));
    }
}