using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Results {
	public interface IResultPowerState {
		public DCCPowerState State { get; }
	}
}