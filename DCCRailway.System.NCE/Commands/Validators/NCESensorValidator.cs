using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands.Validators {
	/// <summary>
	///     This is a special validation just for Sensors. It will either return 2 bytes for the sensors
	///     or will return '0' to includate that the command is not supported
	/// </summary>
	public class NCESensorValidator : IResultValidation {
		public IResult Validate(byte[] data) {
			return data.Length switch {
				0 => new ResultError("Unexpected data returned and not processed. Expected 2 Bytes.", data!),
				1 => data[0] switch {
					(byte)'0' => new ResultError("Command not supported or not in Programming Track mode."),
					(byte)'3' => new ResultError("Data provided is out of range."),
					_ => new ResultError("Unknown response from the NCE System.", data!)
				},
				2 => new ResultOK(data),
				_ => new ResultError("Unknown response from the NCE System.", data!)
			};
		}
	}
}