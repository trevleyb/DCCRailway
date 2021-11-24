using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdLocoSetFunctions : ICommand {
		public IDCCAddress Address { get; set; }
		public DCCFunctionBlocks Functions { get; }
	}
}