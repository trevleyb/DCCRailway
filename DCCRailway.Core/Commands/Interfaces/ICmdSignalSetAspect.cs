using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdSignalSetAspect : ICommand {
		public IDCCAddress Address { get; set; }
		public byte Aspect { get; set; }
		public bool Off { set; }
	}
}