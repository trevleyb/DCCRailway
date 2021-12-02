using DCCRailway.Core.Common;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdLocoStop : ICommand {
		public IDCCAddress Address { get; set; }
	}
}