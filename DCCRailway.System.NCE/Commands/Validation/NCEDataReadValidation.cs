using DCCRailway.Core.Commands;
using DCCRailway.Core.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEDataReadValidation : IResultValidation {
		public IResult Validate(byte[] data) {
			if (data == null || data.Length == 0) return new ResultError("Unexpected data returned and not processed. Expected 2 Bytes.", data!);
			if (data.Length == 1) {
				return data[0] switch {
					(byte)'0' => new ResultError("Command not supported or not in Programming Track mode."),
					(byte)'3' => new ResultError("Data provided is out of range."),
					_ => new ResultError("Unknown response from the NCE System.", data!)
				};
			}
			if (data.Length == 2) {
				return data[1] switch {
					(byte)'0' => new ResultError("Command not supported or not in Programming Track mode."),
					(byte)'3' => new ResultError("Data provided is out of range."),
					(byte)'!' => new ResultOK(data[0]),
					_ => new ResultError("Unknown response from the NCE System.", data!)
				};
			}
			return new ResultError("Unknown response from the NCE System.", data!);
		}
	}
}