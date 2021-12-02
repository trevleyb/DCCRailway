using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdConsistDelete : ICommand {
		public IDCCAddress Address { get; set; }
	}
}