using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Core.Utilities;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands; 

public class NCELocoSetMomentum : NCECommand, ICmdLocoSetMomentum, ICommand {
    public NCELocoSetMomentum() { }

    public NCELocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), momentum) { }

    public NCELocoSetMomentum(IDCCAddress address, byte momentum) {
        Address = address;
        Momentum = momentum;
    }

    public static string Name => "NCE Set Loco Momentum";

    public IDCCAddress Address { get; set; }
    public byte Momentum { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte[] command = {0xA2};
        command = command.AddToArray(((DCCAddress) Address).AddressBytes);
        command = command.AddToArray(0x12);
        command = command.AddToArray(Momentum);
        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() {
        return $"LOCO MOMENTUM ({Address}={Momentum}";
    }
}