using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdLocoSetMomentum : ICommand {
		public IDCCAddress Address { get; set; }
		public byte Momentum { get; set; }
	}
}