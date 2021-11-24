using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Validators;

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