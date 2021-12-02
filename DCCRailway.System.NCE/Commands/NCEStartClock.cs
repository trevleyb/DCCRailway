using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;

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