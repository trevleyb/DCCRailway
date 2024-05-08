using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("LocoSetSpeedSteps", "Set the number of Speed Steps for this Loco")]
public class NCELocoSetSpeedSteps : NCECommand, ICmdLocoSetSpeedSteps, ICommand {
    public NCELocoSetSpeedSteps() { }

    public NCELocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

    public NCELocoSetSpeedSteps(DCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        SpeedSteps = speedSteps;
    }

    public DCCAddress Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        byte[] command = { 0x8D };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray((byte)SpeedSteps);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
}