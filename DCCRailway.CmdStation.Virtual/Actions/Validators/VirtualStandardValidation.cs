using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.CmdStation.Actions.Validators;

namespace DCCRailway.CmdStation.Virtual.Actions.Validators;

public class VirtualStandardValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        return CmdResult.Ok(data ?? []);
    }
}