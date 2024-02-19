using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("LocoSetSpeed", "Set the Speed of a Loco")]
public class NCELocoSetSpeed : NCECommand, ICmdLocoSetSpeed, ICommand {
    public NCELocoSetSpeed() { }

    public NCELocoSetSpeed(IDCCAddress address, DCCDirection direction = DCCDirection.Forward, byte speed = 0, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address    = address;
        Speed      = speed;
        Direction  = direction;
        SpeedSteps = speedSteps;
    }

    public IDCCAddress  Address    { get; set; }
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);

        if (Direction == DCCDirection.Stop) {
            command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x06 : 0x05));
            Speed   = 0;
        } else {
            if (SpeedSteps == DCCProtocol.DCC14 || SpeedSteps == DCCProtocol.DCC28) {
                command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x02 : 0x01));
            } else {
                command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x04 : 0x03));
            }
        }

        command = command.AddToArray(Speed);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO SPEED ({Address}={Direction}@{SpeedSteps}={Speed}";
}