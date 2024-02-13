using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.System.NCE.Commands;

[Command("ConsistAdd", "Add a Loco to a Consist")]
public class NCEConsistAdd : NCECommand, ICmdConsistAdd, ICommand {
    public NCEConsistAdd() { }

    public NCEConsistAdd(byte consistAddress, IDCCLoco loco, DCCConsistPosition position = DCCConsistPosition.Front) {
        Loco           = loco;
        Position       = position;
        ConsistAddress = consistAddress;
    }

    public NCEConsistAdd(byte consistAddress, IDCCAddress address, DCCDirection direction = DCCDirection.Forward, DCCConsistPosition position = DCCConsistPosition.Front) : this(consistAddress, new DCCLoco(address, direction), position) { }

    public byte               ConsistAddress { get; set; }
    public IDCCLoco           Loco           { get; set; }
    public DCCConsistPosition Position       { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Loco.Address.AddressBytes);

        command = Position switch {
            DCCConsistPosition.Front => command.AddToArray((byte)(Loco.Direction == DCCDirection.Forward ? 0x0b : 0x0a)),
            DCCConsistPosition.Rear  => command.AddToArray((byte)(Loco.Direction == DCCDirection.Forward ? 0x0d : 0x0c)),
            _                        => command.AddToArray((byte)(Loco.Direction == DCCDirection.Forward ? 0x0f : 0x0e))
        };
        command = command.AddToArray(ConsistAddress);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST ADD TO {ConsistAddress:D3} @ {Position} ({Loco.Address}={Loco.Direction})";
}