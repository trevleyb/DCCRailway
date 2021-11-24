using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	/// <summary>
	///     Read a CV from a system
	/// </summary>
	public interface ICmdCVRead : ICommand {
		public DCCProgrammingMode ProgrammingMode { get; set; }
		public int CV { get; set; }
	}
}