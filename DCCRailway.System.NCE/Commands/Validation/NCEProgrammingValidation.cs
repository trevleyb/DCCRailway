using DCCRailway.Core.Commands;
using DCCRailway.Core.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEProgrammingValidation : IResultValidation {
		public IResult Validate(byte[] data) {
			if (data == null || data.Length != 0) return new ResultError("Unexpected data returned and not processed.", data!);
			return data[0] switch {
				(byte)'0' => new ResultError("Programming Track is not enabled."),
				(byte)'3' => new ResultError("Short Circuit detected on the track."),
				(byte)'!' => new ResultOK(),
				_ => new ResultError("Unknown response from the NCE System.", data!)
			};
		}
	}
}