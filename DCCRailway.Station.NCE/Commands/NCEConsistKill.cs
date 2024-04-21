using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class NCEConsistKill : NCECommand, ICmdConsistKill, ICommand {
    public NCEConsistKill() { }

    public NCEConsistKill(DCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x11);
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST KILL ({Address})";
}