using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands.Validator;

public class SimpleResultValidation : IResultValidation {
    public SimpleResultValidation(int expectedLength) => LengthExpected = expectedLength;

    public int LengthExpected { get; set; }

    public CommandResult Validate(byte[] dataRecieved) {
        if (dataRecieved == null && LengthExpected != 0) return CommandResult.Fail("Expected data from the Adapter but recieved null.");
        if (LengthExpected == 0 && dataRecieved?.Length != 0) return CommandResult.Fail("No data was expected, but data was recieved.", new CommandResultDataSet(dataRecieved!));
        if (dataRecieved?.Length == 0 && LengthExpected > 0) return CommandResult.Fail($"No data was recieved but {LengthExpected} was expected.");
        if (dataRecieved?.Length > LengthExpected) return CommandResult.Fail($"Too much data was recieved. Expected {LengthExpected} but recieved {dataRecieved?.Length}", new CommandResultDataSet(dataRecieved!));
        if (dataRecieved?.Length < LengthExpected) return CommandResult.Fail($"Not enough data was recieved. Expected {LengthExpected} but only recieved {dataRecieved?.Length}", new CommandResultDataSet(dataRecieved)!);
        return CommandResult.Ok(new CommandResultDataSet(dataRecieved!));
    }
}