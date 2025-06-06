﻿using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("MacroRun", "Execute a Macro")]
public class NCEMacroRun : NCECommand, ICmdMacroRun {
    public byte Macro { get; set; }

    protected override ICmdResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0xAC, Macro });
    }

    public override string ToString() {
        return $"RUN MACRO ({Macro})";
    }
}