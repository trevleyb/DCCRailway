﻿using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.System.NCE.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class NCELocoSetMomentum : NCECommand, ICmdLocoSetMomentum, ICommand {
    public NCELocoSetMomentum() { }

    public NCELocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), momentum) { }

    public NCELocoSetMomentum(IDCCAddress address, byte momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public IDCCAddress Address  { get; set; }
    public byte        Momentum { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(((DCCAddress)Address).AddressBytes);
        command = command.AddToArray(0x12);
        command = command.AddToArray(Momentum);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"LOCO MOMENTUM ({Address}={Momentum}";
}