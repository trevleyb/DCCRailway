using DCCRailway.Core.Common;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdConsistDelete : ICommand {
		public IDCCAddress Address { get; set; }
	}
}