using DCCRailway.Core.Common;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdAccySetState : ICommand {
		public IDCCAddress Address { get; set; }
		public DCCAccessoryState State { get; set; }
	}
}