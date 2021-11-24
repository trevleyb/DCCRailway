using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEStartClock : NCECommandBase, ICmdClockStart, ICommand {
		public string Name {
			get { return "NCE Start Clock"; }
		}

		public override IResult Execute(IAdapter adapter) {
			return SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] { 0x84 });
		}

		public override string ToString() {
			return "START CLOCK ";
		}
	}
}