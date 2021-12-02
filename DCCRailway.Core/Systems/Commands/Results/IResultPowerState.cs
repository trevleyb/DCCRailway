using DCCRailway.Core.Common;

namespace DCCRailway.Core.Systems.Commands.Results {
	public interface IResultPowerState {
		public DCCPowerState State { get; }
	}
}