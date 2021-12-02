using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEReadClock : CommandBase, ICmdClockRead, ICommand {
		public static string Name {
			get { return "NCE Read Fast Clock"; }
		}

		public override IResult Execute(IAdapter adapter) {
			var result = SendAndReceieve(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });
			if (!result.OK) return result;
			return new NCEClockReadResult(result.Data);
		}

		public override string ToString() {
			return "READ CLOCK";
		}
	}
}