﻿using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Layout.Commands;
using DCCRailway.System.Layout.Commands.Results;
using DCCRailway.System.Layout.Commands.Types;
using DCCRailway.System.Layout.Types;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.NCE.Commands;

[Command("ConsistKill", "Remove a whole Consist")]
public class NCEConsistKill : NCECommand, ICmdConsistKill, ICommand {
    public NCEConsistKill() { }

    public NCEConsistKill(IDCCLoco loco) : this(loco.Address) { }

    public NCEConsistKill(IDCCAddress address) => Address = address;

    public IDCCAddress Address { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte[] command = { 0xA2 };
        command = command.AddToArray(Address.AddressBytes);
        command = command.AddToArray(0x11);
        command = command.AddToArray(0);

        return SendAndReceive(adapter, new NCEStandardValidation(), command);
    }

    public override string ToString() => $"CONSIST KILL ({Address})";
}