using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class NCELocoSetMomentum : NCECommand, ICmdLocoSetMomentum, ICommand {
    public NCELocoSetMomentum() { }

    public NCELocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), new DCCMomentum(momentum)) { }

    public NCELocoSetMomentum(DCCAddress address, DCCMomentum momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public DCCAddress Address  { get; set; }
    public DCCMomentum Momentum { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray(0x12);
        command = command.AddToArray(Momentum.Value);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO MOMENTUM ({Address}={Momentum}";
}