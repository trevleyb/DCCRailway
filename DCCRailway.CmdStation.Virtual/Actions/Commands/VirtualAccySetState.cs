﻿using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("AccySetState", "Set the state of an Accessory")]
public class VirtualAccySetState : VirtualCommand, ICmdAccySetState, ICommand {
    public VirtualAccySetState() { }

    public VirtualAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) => State = state;

    public DCCAddress       Address { get; set; }
    public DCCAccessoryState State   { get; set; }

    public override string ToString() => $"ACCY STATE ({Address} = {State})";
}