namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdRosterCmd : ThrottleCmdBase, IThrottleCmd {
		public CmdRosterCmd(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		public string? Execute() {
			return null;
		}

		public override string ToString() {
			return "COMMAND: ROSTER COMMAND";
		}
	}
}