﻿using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;
using DCCRailway.Utilities.Exceptions;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("CVRead", "Read a CV from a Loco")]
public class VirtualCVRead : VirtualCommand, ICmdCVRead, ICommand {
    public VirtualCVRead(int cv = 0) => CV = cv;

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct   => 0xA9,
            DCCProgrammingMode.Paged    => 0xA1,
            DCCProgrammingMode.Register => 0xA7,
            _                           => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceive(adapter, new VirtualDataReadValidation(), CV.ToByteArray().AddToArray(command));
    }

    public override string ToString() => $"READ CV ({CV}/{ProgrammingMode})";
}