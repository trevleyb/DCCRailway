using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;

namespace DCCRailway.CmdStation.Commands.Validators;

public class SimpleResultValidation : IResultValidation {
    public SimpleResultValidation(int expectedLength) => LengthExpected = expectedLength;

    public int LengthExpected { get; set; }
    public ICmdResult Validate(byte[]? dataReceived) {
        if (dataReceived == null && LengthExpected != 0) return CmdResult.Fail("Expected data from the Adapter but recieved null.");
        if (LengthExpected == 0 && dataReceived?.Length != 0) return CmdResult.Fail(dataReceived!, "No data was expected, but data was recieved.");
        if (dataReceived?.Length == 0 && LengthExpected > 0) return CmdResult.Fail($"No data was recieved but {LengthExpected} was expected.");
        if (dataReceived?.Length > LengthExpected) return CmdResult.Fail(dataReceived!, $"Too much data was recieved. Expected {LengthExpected} but recieved {dataReceived?.Length}");
        if (dataReceived?.Length < LengthExpected) return CmdResult.Fail(dataReceived!, $"Not enough data was recieved. Expected {LengthExpected} but only recieved {dataReceived?.Length}");
        return CmdResult.Ok(dataReceived!);
    }
}