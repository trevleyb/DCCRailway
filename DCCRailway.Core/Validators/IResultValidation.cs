using DCCRailway.Core.Commands;

namespace DCCRailway.Core.Validators {
	public interface IResultValidation {
		public IResult Validate(byte[] data);
	}
}