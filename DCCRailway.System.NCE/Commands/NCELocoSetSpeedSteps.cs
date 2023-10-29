using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.NCE.Commands;

[Command("LocoSetSpeedSteps", "Set the number of Speed Steps for this Loco")]
public class NCELocoSetSpeedSteps : NCECommand, ICmdLocoSetSpeedSteps, ICommand {
    public NCELocoSetSpeedSteps() { }

    public NCELocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

    public NCELocoSetSpeedSteps(IDCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        SpeedSteps = speedSteps;
    }

    public IDCCAddress Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte[] command = { 0x8D };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray((byte)SpeedSteps);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
}