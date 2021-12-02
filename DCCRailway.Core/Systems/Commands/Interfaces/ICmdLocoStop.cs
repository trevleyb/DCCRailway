using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdLocoStop : ICommand {
		public IDCCAddress Address { get; set; }
	}
}