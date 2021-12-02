using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Core.Systems.Commands.Validators {
	public interface IResultValidation {
		public IResult Validate(byte[] data);
	}
}