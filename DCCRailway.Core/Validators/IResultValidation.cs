using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Core.Validators {
	public interface IResultValidation {
		public IResult Validate(byte[] data);
	}
}