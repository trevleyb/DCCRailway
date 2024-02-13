﻿using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Layout.Commands;
using DCCRailway.System.Layout.Commands.Results;
using DCCRailway.System.Layout.Commands.Types;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class NCESetMainTrk : NCECommand, ICmdTrackMain {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9F);

    public override string ToString() => "MAIN TRACK";
}