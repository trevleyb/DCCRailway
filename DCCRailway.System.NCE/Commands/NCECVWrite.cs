﻿using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Exceptions;
using DCCRailway.System.Utilities;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

[Command("CVWrite", "Write a value to a CV on a Loco")]
public class NCECVWrite : NCECommand, ICmdCVWrite, ICommand {
    public NCECVWrite(int cv = 0, byte value = 0) {
        CV = cv;
        Value = value;
    }
    
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int CV { get; set; }
    public byte Value { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct => 0xA8,
            DCCProgrammingMode.Paged => 0xA0,
            DCCProgrammingMode.Register => 0xA6,
            _ => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceieve(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command).AddToArray(Value));
    }

    public override string ToString() {
        return $"WRITE CV ({CV}={Value}/{ProgrammingMode})";
    }
}