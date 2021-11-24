using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Utilities;
using DCCRailway.Core.Validators;

namespace DCCRailway.Systems.Virtual.Commands {
	public class VirtualStatus : CommandBase, ICmdStatus {
		private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

		public static string Name {
			get { return "Virtual Status Command"; }
		}

		public override IResult Execute(IAdapter adapter) {
			var result = SendAndReceieve(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());
			if (!result.OK) return result;
			return new ResultOK(result.Data);
		}
	}
}