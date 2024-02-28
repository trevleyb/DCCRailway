﻿using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("MacroRun", "Execute a Macro")]
public class VirtualMacroRun : VirtualCommand, ICmdMacroRun {
    public byte Macro { get; set; }
    public override string ToString() => $"RUN MACRO ({Macro})";
}