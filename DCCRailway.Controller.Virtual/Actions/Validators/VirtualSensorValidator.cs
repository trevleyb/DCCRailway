using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Actions.Validators;

namespace DCCRailway.Controller.Virtual.Actions.Validators;

/// <summary>
///     This is a special validation just for Sensors. It will either return 2 bytes for the sensors
///     or will return '0' to includate that the command is not supported
/// </summary>
public class VirtualSensorValidator : IResultValidation {
    public ICmdResult Validate(byte[]? data) => data?.Length switch {
        0 => CmdResult.Fail(data!, "Unexpected data returned and not processed. Expected 2 Bytes."),
        1 => data[0] switch {
            (byte)'0' => CmdResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CmdResult.Fail("Data provided is out of range."),
            _         => CmdResult.Fail(data!, "Unknown response from the Virtual CommandStation.")
        },
        2 => CmdResult.Ok(data),
        _ => CmdResult.Fail(data!, "Unknown response from the Virtual CommandStation.")
    } ?? CmdResult.Fail("Invalid data returned from the command execution.");
}