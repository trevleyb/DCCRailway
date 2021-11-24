using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdLocoStop : ICommand {
		public IDCCAddress Address { get; set; }
	}
}