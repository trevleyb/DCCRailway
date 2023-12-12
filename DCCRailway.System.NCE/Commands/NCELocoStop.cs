using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.NCE.Commands;

[Command("LocoStop", "Stop the Loco")]
public class NCELocoStop : NCECommand, ICmdLocoStop, ICommand {
    public NCELocoStop() { }

    public NCELocoStop(IDCCLoco loco) : this(loco.Address, loco.Direction) { }

    public NCELocoStop(int address, DCCDirection direction = DCCDirection.Forward) : this(new DCCAddress(address), direction) { }

    public NCELocoStop(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) {
        Address   = address;
        Direction = direction;
    }

    public DCCDirection Direction { get; set; }

    public IDCCAddress Address { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x06 : 0x05));
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO STOP ({Address}={Direction}";
}