﻿using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands;

[Command("MacroRun", "Execute a Macro")]
public class NCEMacroRun : NCECommand, ICmdMacroRun {
    public byte Macro { get; set; }

    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0xAC, Macro });

    public override string ToString() => $"RUN MACRO ({Macro})";
}