using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdConsistAdd : ICommand {
		public byte ConsistAddress { get; set; }
		public IDCCLoco Loco { get; set; }
		public DCCConsistPosition Position { get; set; }
	}
}