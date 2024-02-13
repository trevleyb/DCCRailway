using DCCRailway.System.Layout.Commands.Results;

namespace DCCRailway.System.Layout.Commands.Validators;

public class SimpleResultValidation : IResultValidation {
    public SimpleResultValidation(int expectedLength) => LengthExpected = expectedLength;

    public int LengthExpected { get; set; }

    public ICommandResult Validate(byte[]? dataReceived) {
        if (dataReceived == null && LengthExpected != 0) return CommandResult.Fail("Expected data from the Adapter but recieved null.");
        if (LengthExpected == 0 && dataReceived?.Length != 0) return CommandResult.Fail("No data was expected, but data was recieved.", new CommandResultData(dataReceived!));
        if (dataReceived?.Length == 0 && LengthExpected > 0) return CommandResult.Fail($"No data was recieved but {LengthExpected} was expected.");
        if (dataReceived?.Length > LengthExpected) return CommandResult.Fail($"Too much data was recieved. Expected {LengthExpected} but recieved {dataReceived?.Length}", new CommandResultData(dataReceived!));
        if (dataReceived?.Length < LengthExpected) return CommandResult.Fail($"Not enough data was recieved. Expected {LengthExpected} but only recieved {dataReceived?.Length}", new CommandResultData(dataReceived)!);
        return CommandResult.Success(new CommandResultData(dataReceived!));
    }
}