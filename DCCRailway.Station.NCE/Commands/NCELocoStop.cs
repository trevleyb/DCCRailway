using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("LocoStop", "Stop the Loco")]
public class NCELocoStop : NCECommand, ICmdLocoStop, ICommand {
    public NCELocoStop() { }

    public NCELocoStop(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) {
        Address   = address;
        Direction = direction;
    }

    public DCCDirection Direction { get; set; }
    public IDCCAddress  Address   { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = [0xA2];
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x06 : 0x05));
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO STOP ({Address}={Direction}";
}