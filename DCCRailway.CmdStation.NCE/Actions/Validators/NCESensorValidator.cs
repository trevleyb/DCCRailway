using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.CmdStation.Commands.Validators;

namespace DCCRailway.CmdStation.NCE.Commands.Validators;

/// <summary>
///     This is a special validation just for Sensors. It will either return 2 bytes for the sensors
///     or will return '0' to includate that the command is not supported
/// </summary>
public class NCESensorValidator : IResultValidation {
    public ICmdResult Validate(byte[]? data) => data?.Length switch {
        0 => CmdResult.Fail(data!,"Unexpected data returned and not processed. Expected 2 Bytes."),
        1 => data[0] switch {
            (byte)'0' => CmdResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CmdResult.Fail("Data provided is out of range."),
            _         => CmdResult.Fail(data!,"Unknown response from the NCE Controller.")
        },
        2 => CmdResult.Ok(data),
        _ => CmdResult.Fail(data!,"Unknown response from the NCE Controller.")
    } ?? CmdResult.Fail("Invalid data returned from the command execution.");
}