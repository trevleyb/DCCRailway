using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Actions.Validators;

namespace DCCRailway.Controller.Virtual.Actions.Validators;

public class VirtualStandardValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        return CmdResult.Ok(data ?? []);
    }
}