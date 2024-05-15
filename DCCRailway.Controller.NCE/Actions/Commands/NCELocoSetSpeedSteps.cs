using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("LocoSetSpeedSteps", "Set the number of Speed Steps for this Loco")]
public class NCELocoSetSpeedSteps : NCECommand, ICmdLocoSetSpeedSteps, ICommand {
    public NCELocoSetSpeedSteps() { }

    public NCELocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

    public NCELocoSetSpeedSteps(DCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        SpeedSteps = speedSteps;
    }

    public DCCAddress  Address    { get; set; }
    public DCCProtocol SpeedSteps { get; set; }

    protected override ICmdResult Execute(IAdapter adapter) {
        byte[] command = { 0x8D };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray((byte)SpeedSteps);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
}