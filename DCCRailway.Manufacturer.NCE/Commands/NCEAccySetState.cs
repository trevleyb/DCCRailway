﻿using DCCRailway.Manufacturer.NCE.Commands.Validators;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("AccySetState", "Set the state of an Accessory")]
public class NCEAccySetState : NCECommand, ICmdAccySetState, ICommand {
    public NCEAccySetState() { }

    public NCEAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) => State = state;

    public IDCCAddress       Address { get; set; }
    public DCCAccessoryState State   { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAD };                                             // Command is 0xAD
        cmd = cmd.AddToArray(((DCCAddress)Address).AddressBytes);                  // Add the high and low bytes of the Address
        cmd = cmd.AddToArray((byte)(State == DCCAccessoryState.On ? 0x03 : 0x04)); // Normal=0x03, Thrown=0x04
        cmd = cmd.AddToArray(0);                                                   // Accessory always has a data of 0x00

        return SendAndReceive(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() => $"ACCY STATE ({Address} = {State})";
}