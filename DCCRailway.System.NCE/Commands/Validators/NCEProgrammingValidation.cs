﻿using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.System.NCE.Commands.Validators;

public class NCEProgrammingValidation : IResultValidation {
    public IResult Validate(byte[] data) {
        if (data.Length != 1) return new ResultError("Unexpected data returned and not processed.", data!);

        return data[0] switch {
            (byte)'0' => new ResultError("Programming Track is not enabled."),
            (byte)'3' => new ResultError("Short Circuit detected on the track."),
            (byte)'!' => new ResultOK(),
            _ => new ResultError("Unknown response from the NCE System.", data!)
        };
    }
}