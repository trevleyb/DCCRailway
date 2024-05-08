using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.CmdStation.Commands.Validators;

namespace DCCRailway.CmdStation.Virtual.Commands.Validators;

public class VirtualStandardValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        return CmdResult.Ok(data ?? []);
    }
}