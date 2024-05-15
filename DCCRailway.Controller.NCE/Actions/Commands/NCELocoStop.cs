using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("LocoStop", "Stop the Loco")]
public class NCELocoStop : NCECommand, ICmdLocoStop, ICommand {
    public NCELocoStop() { }

    public NCELocoStop(DCCAddress address, DCCDirection direction = DCCDirection.Forward) {
        Address   = address;
        Direction = direction;
    }

    public DCCDirection Direction { get; set; }
    public DCCAddress   Address   { get; set; }

    protected override ICmdResult Execute(IAdapter adapter) {
        byte[] command = [0xA2];
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x06 : 0x05));
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO STOP ({Address}={Direction}";
}