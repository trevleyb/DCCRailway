using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdConsistAdd : ICommand {
		public byte ConsistAddress { get; set; }
		public IDCCLoco Loco { get; set; }
		public DCCConsistPosition Position { get; set; }
	}
}