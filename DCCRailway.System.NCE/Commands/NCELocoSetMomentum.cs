using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Utilities;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class NCELocoSetMomentum : NCECommand, ICmdLocoSetMomentum, ICommand {
    public NCELocoSetMomentum() { }

    public NCELocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), momentum) { }

    public NCELocoSetMomentum(IDCCAddress address, byte momentum) {
        Address = address;
        Momentum = momentum;
    }
    
    public IDCCAddress Address { get; set; }
    public byte Momentum { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray(0x12);
        command = command.AddToArray(Momentum);

        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() {
        return $"LOCO MOMENTUM ({Address}={Momentum}";
    }
}