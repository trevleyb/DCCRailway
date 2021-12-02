using DCCRailway.Core.Common;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdSensorGetState : ICommand {
		public IDCCAddress SensorAddress { get; set; }
	}
}