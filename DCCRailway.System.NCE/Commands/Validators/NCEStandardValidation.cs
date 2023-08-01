namespace DCCRailway.System.NCE.Commands.Validators;

public class NCEStandardValidation : IResultValidation {
    public IResult Validate(byte[] data) {
        // Standard result codes from the NCE system are as follows:
        // '0' = command not supported
        // '1' = loco address out of range
        // '2' = cab address out of range
        // '3' = data out of range
        // '4' = byte count out of range
        // '!' = command completed successfully
        if (data.Length != 1) return new ResultError("Unexpected data returned and not processed.", data!);

        return data[0] switch {
            (byte)'0' => new ResultError("Command not supported."),
            (byte)'1' => new ResultError("Loco address is out of range."),
            (byte)'2' => new ResultError("Cab address is out of range."),
            (byte)'3' => new ResultError("Data provided is out of range."),
            (byte)'4' => new ResultError("Byte count is out of range."),
            (byte)'!' => new ResultOK(data),
            _ => new ResultError("Unknown response from the NCE System.", data!)
        };
    }
}