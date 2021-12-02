using DCCRailway.Core.Common;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdConsistKill : ICommand {
		public IDCCAddress Address { get; set; }
	}
}