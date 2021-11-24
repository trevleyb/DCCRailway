using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	/// <summary>
	///     Write a value to a specific CV
	/// </summary>
	public interface ICmdCVWrite : ICommand {
		public DCCProgrammingMode ProgrammingMode { get; set; }
		public int CV { get; set; }
		public byte Value { get; set; }
	}
}