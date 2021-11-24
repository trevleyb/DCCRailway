using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdConsistDelete : ICommand {
		public IDCCAddress Address { get; set; }
	}
}