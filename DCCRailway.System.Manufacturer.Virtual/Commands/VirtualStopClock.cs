﻿using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("StopClock", "Stop the Virtual Clock")]
public class VirtualStopClock : VirtualCommand, ICmdClockStop, ICommand {
    public override string ToString() => "STOP CLOCK";
}