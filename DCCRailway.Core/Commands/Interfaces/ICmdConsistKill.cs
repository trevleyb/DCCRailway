using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdConsistKill : ICommand {
		public IDCCAddress Address { get; set; }
	}
}