using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Validators;

public class NCEStandardValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        // Standard resultOld codes from the NCE controller are as follows:
        // '0' = command not supported
        // '1' = loco address out of range
        // '2' = cab address out of range
        // '3' = data out of range
        // '4' = byte count out of range
        // '!' = command completed successfully
        if (data != null && data.Length != 1)
            return CmdResult.Fail(data!, "Unexpected data returned and not processed.");

        return data?[0] switch {
            (byte)'0' => CmdResult.Fail("Command not supported."),
            (byte)'1' => CmdResult.Fail("Loco address is out of range."),
            (byte)'2' => CmdResult.Fail("Cab address is out of range."),
            (byte)'3' => CmdResult.Fail("Data provided is out of range."),
            (byte)'4' => CmdResult.Fail("Byte count is out of range."),
            (byte)'!' => CmdResult.Ok(data),
            _         => CmdResult.Fail(data!, "Unknown response from the NCE CommandStation.")
        } ?? CmdResult.Fail("Invalid data returned from the command execution.");
    }
}