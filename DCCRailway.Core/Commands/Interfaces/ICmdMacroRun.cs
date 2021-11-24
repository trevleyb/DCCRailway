namespace DCCRailway.Core.Commands {
	public interface ICmdMacroRun : ICommand {
		public byte Macro { get; set; }
	}
}