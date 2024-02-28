﻿using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

[Command("MacroRun", "Execute a Macro")]
public class VirtualMacroRun : VirtualCommand, ICmdMacroRun {
    public byte Macro { get; set; }
    public override string ToString() => $"RUN MACRO ({Macro})";
}