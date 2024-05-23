using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Actions.Validators;

namespace DCCRailway.Controller.Virtual.Actions.Validators;

public class VirtualDataReadValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        return data?.Length switch {
            0 => CmdResult.Fail(data!, "Unexpected data returned and not processed. Expected 2 Bytes."),
            1 => data[0] switch {
                (byte)'0' => CmdResult.Fail("Command not supported or not in Programming Track mode."),
                (byte)'3' => CmdResult.Fail("Data provided is out of range."),
                _         => CmdResult.Fail(data!, "Unknown response from the Virtual CommandStation.")
            },
            2 => data[1] switch {
                (byte)'0' => CmdResult.Fail("Command not supported or not in Programming Track mode."),
                (byte)'3' => CmdResult.Fail("Data provided is out of range."),
                (byte)'!' => CmdResult.Ok(data!),
                _         => CmdResult.Fail(data!, "Unknown response from the Virtual CommandStation.")
            },
            _ => CmdResult.Fail(data!, "Unknown response from the Virtual CommandStation.")
        } ?? CmdResult.Fail("No data returned from the command execution.");
    }
}