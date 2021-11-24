using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdSensorGetState : ICommand {
		public IDCCAddress SensorAddress { get; set; }
	}
}