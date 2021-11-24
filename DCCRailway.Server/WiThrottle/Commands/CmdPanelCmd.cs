namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdPanelCmd : ThrottleCmdBase, IThrottleCmd {
		public CmdPanelCmd(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		public string? Execute() {
			return null;
		}

		public override string ToString() {
			return "COMMAND: PANEL COMMAND";
		}
	}
}