using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdLocoSetSpeed : ICommand {
		public IDCCAddress Address { get; set; }
		public DCCProtocol SpeedSteps { get; set; }
		public DCCDirection Direction { get; set; }
		public byte Speed { get; set; }
	}
}