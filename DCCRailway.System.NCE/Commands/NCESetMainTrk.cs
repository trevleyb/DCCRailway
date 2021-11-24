using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCESetMainTrk : NCECommandBase, ICmdTrackMain {
		public string Name {
			get { return "NCE Switch to the Main Track"; }
		}

		public override IResult Execute(IAdapter adapter) {
			return SendAndReceieve(adapter, new NCEStandardValidation(), 0x9F);
		}

		public override string ToString() {
			return "MAIN TRACK";
		}
	}
}