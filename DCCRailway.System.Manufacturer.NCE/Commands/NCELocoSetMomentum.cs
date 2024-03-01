using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.System.Manufacturer.NCE.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class NCELocoSetMomentum : NCECommand, ICmdLocoSetMomentum, ICommand {
    public NCELocoSetMomentum() { }

    public NCELocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), new DCCMomentum(momentum)) { }

    public NCELocoSetMomentum(IDCCAddress address, DCCMomentum momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public IDCCAddress Address  { get; set; }
    public DCCMomentum Momentum { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray(0x12);
        command = command.AddToArray(Momentum.Value);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO MOMENTUM ({Address}={Momentum}";
}