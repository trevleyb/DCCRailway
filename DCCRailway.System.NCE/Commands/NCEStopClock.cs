using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEStopClock : NCECommandBase, ICmdClockStop, ICommand {
		public string Name {
			get { return "NCE Stop Clock"; }
		}

		public override IResult Execute(IAdapter adapter) {
			return SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] { 0x83 });
		}

		public override string ToString() {
			return "STOP CLOCK";
		}
	}
}