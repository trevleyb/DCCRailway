using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("ConsistDelete", "Remove a Loco from a Consist")]
public class NCEConsistDelete : NCECommand, ICmdConsistDelete, ICommand {
    public NCEConsistDelete() { }

    public NCEConsistDelete(DCCAddress address) => Address = address;

    public DCCAddress Address { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x10);
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST DELETE ({Address})";
}