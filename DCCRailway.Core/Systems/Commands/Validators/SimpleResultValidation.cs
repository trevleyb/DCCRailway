using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Core.Systems.Commands.Validators {
	public class SimpleResultValidation : IResultValidation {
		public SimpleResultValidation(int expectedLength) {
			LengthExpected = expectedLength;
		}

		public int LengthExpected { get; set; }

		public IResult Validate(byte[] dataRecieved) {
			if (dataRecieved == null && LengthExpected != 0) return new ResultError("Expected data from the Adapter but recieved null.");

			if (LengthExpected == 0 && dataRecieved?.Length != 0) return new ResultError("No data was expected, but data was recieved.", dataRecieved!);
			if (dataRecieved?.Length == 0 && LengthExpected > 0) return new ResultError($"No data was recieved but {LengthExpected} was expected.");
			if (dataRecieved?.Length > LengthExpected) {
				return new ResultError($"Too much data was recieved. Expected {LengthExpected} but recieved {dataRecieved?.Length}", dataRecieved!);
			}
			if (dataRecieved?.Length < LengthExpected) {
				return new ResultError($"Not enough data was recieved. Expected {LengthExpected} but only recieved {dataRecieved?.Length}", dataRecieved!);
			}
			return new ResultOK(dataRecieved);
		}
	}
}