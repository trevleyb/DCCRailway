using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEStatusCmd : NCECommandBase, ICmdStatus {
		public static string Name {
			get { return "NCE Get Status"; }
		}

		public override IResult Execute(IAdapter adapter) {
			var result = SendAndReceieve(adapter, new SimpleResultValidation(3), new byte[] { 0xAA });
			if (!result.OK) return result;
			return new NCEStatusResult(result.Data);
		}

		public override string ToString() {
			return "GET STATUS";
		}
	}
}