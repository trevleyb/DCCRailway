using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

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