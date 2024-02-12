using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;
using DCCRailway.System.Virtual.Adapters;
using DCCRailway.System.Virtual.Commands;

namespace DCCRailway.System.Virtual;

[System("Virtual", "Virtual", "Virtual")]
public class VirtualController : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<VirtualAdapter>();
    }

    protected override void RegisterCommands() {
        RegisterCommand<IDummyCmd>(typeof(VirtualDummy));
        RegisterCommand<ICmdStatus>(typeof(VirtualStatus));
    }
}