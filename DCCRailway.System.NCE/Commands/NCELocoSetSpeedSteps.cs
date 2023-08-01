﻿using DCCRailway.Core.Utilities;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCELocoSetSpeedSteps : NCECommand, ICmdLocoSetSpeedSteps, ICommand {
    public NCELocoSetSpeedSteps() { }

    public NCELocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

    public NCELocoSetSpeedSteps(IDCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
        Address = address;
        SpeedSteps = speedSteps;
    }

    public static string Name => "NCE Set Loco Speed Steps";

    public IDCCAddress Address { get; set; }
    public DCCProtocol SpeedSteps { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte[] command = { 0x8D };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray((byte)SpeedSteps);

        return SendAndReceieve(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() {
        return $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
    }
}