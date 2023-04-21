using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Core.Utilities;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands; 

public class NCELocoSetSpeed : NCECommand, ICmdLocoSetSpeed, ICommand {
    public NCELocoSetSpeed() { }

    public NCELocoSetSpeed(IDCCLoco loco, byte speed = 0) : this(loco.Address, loco.Direction, speed) { }

    public NCELocoSetSpeed(int address, DCCDirection direction = DCCDirection.Forward, byte speed = 0, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), direction, speed, speedSteps) { }

    public NCELocoSetSpeed(IDCCAddress address, DCCDirection direction = DCCDirection.Forward, byte speed = 0, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address = address;
        Speed = speed;
        Direction = direction;
        SpeedSteps = speedSteps;
    }

    public static string Name => "NCE Set Loco Speed";

    public IDCCAddress Address { get; set; }
    public DCCProtocol SpeedSteps { get; set; }
    public DCCDirection Direction { get; set; }
    public byte Speed { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte[] command = {0xA2};
        command = command.AddToArray(((DCCAddress) Address).AddressBytes);
        if (Direction == DCCDirection.Stop) {
            command = command.AddToArray((byte) (Direction == DCCDirection.Forward ? 0x06 : 0x05));
            Speed = 0;
        }
        else {
            if (SpeedSteps == DCCProtocol.DCC14 || SpeedSteps == DCCProtocol.DCC28)
                command = command.AddToArray((byte) (Direction == DCCDirection.Forward ? 0x02 : 0x01));
            else
                command = command.AddToArray((byte) (Direction == DCCDirection.Forward ? 0x04 : 0x03));
        }

        command = command.AddToArray(Speed);
        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() {
        return $"LOCO SPEED ({Address}={Direction}@{SpeedSteps}={Speed}";
    }
}