using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.CmdStation.Actions.Validators;

namespace DCCRailway.CmdStation.Virtual.Actions.Validators;

public class VirtualDataReadValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) => data?.Length switch {
        0 => CmdResult.Fail(data!,"Unexpected data returned and not processed. Expected 2 Bytes."),
        1 => data[0] switch {
            (byte)'0' => CmdResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CmdResult.Fail("Data provided is out of range."),
            _         => CmdResult.Fail(data!,"Unknown response from the Virtual Controller.")
        },
        2 => data[1] switch {
            (byte)'0' => CmdResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CmdResult.Fail("Data provided is out of range."),
            (byte)'!' => CmdResult.Ok(data!),
            _         => CmdResult.Fail(data!,"Unknown response from the Virtual Controller.")
        },
        _ => CmdResult.Fail(data!,"Unknown response from the Virtual Controller.")
    } ?? CmdResult.Fail("No data returned from the command execution.");
}