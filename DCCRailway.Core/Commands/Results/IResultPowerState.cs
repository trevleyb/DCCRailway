using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface IResultPowerState {
		public DCCPowerState State { get; }
	}
}