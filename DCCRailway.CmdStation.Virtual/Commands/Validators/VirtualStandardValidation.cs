using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Validators;

namespace DCCRailway.CmdStation.Virtual.Commands.Validators;

public class VirtualStandardValidation : IResultValidation {
    public ICommandResult Validate(byte[]? data) {
        return CommandResult.Success(data ?? []);
    }
}