using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdLocoSetSpeedSteps : ICommand {
		public IDCCAddress Address { get; set; }
		public DCCProtocol SpeedSteps { get; set; }
	}
}