using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Controllers;
using DCCRailway.System.Virtual.Adapters;
using DCCRailway.System.Virtual.Commands;

namespace DCCRailway.System.Virtual;

[Controller("Virtual", "Virtual", "Virtual")]
public class VirtualController : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<VirtualAdapter>();
    }

    protected override void RegisterCommands() {
        RegisterCommand<IDummyCmd>(typeof(VirtualDummyCmd));
        RegisterCommand<ICmdStatus>(typeof(VirtualStatusCmd));
        RegisterCommand<ICmdLocoSetSpeed>(typeof(VirtualLocoSetSpeed));
    }
}