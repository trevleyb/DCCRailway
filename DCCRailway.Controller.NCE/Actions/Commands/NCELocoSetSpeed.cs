using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("LocoSetSpeed", "Set the Speed of a Loco")]
public class NCELocoSetSpeed : NCECommand, ICmdLocoSetSpeed, ICommand {
    public NCELocoSetSpeed() { }

    public NCELocoSetSpeed(DCCAddress address, DCCDirection direction = DCCDirection.Forward, DCCSpeed? speed = null, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        Speed      = speed ?? new DCCSpeed(0);
        Direction  = direction;
        SpeedSteps = speedSteps;
    }

    public DCCAddress  Address    { get; set; }
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);

        if (Direction == DCCDirection.Stop) {
            command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x06 : 0x05));
            Speed   = new DCCSpeed(0);
        }
        else {
            if (SpeedSteps == DCCProtocol.DCC14 || SpeedSteps == DCCProtocol.DCC28)
                command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x02 : 0x01));
            else
                command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x04 : 0x03));
        }

        command = command.AddToArray(Speed.Value);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO SPEED ({Address}={Direction}@{SpeedSteps}={Speed}";
}