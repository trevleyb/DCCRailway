using System;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Validators;

namespace DCCRailway.Station.Virtual.Commands.Validators;

public class VirtualStandardValidation : IResultValidation {
    public ICommandResult Validate(byte[]? data) {
        return CommandResult.Success(data ?? []);
    }
}