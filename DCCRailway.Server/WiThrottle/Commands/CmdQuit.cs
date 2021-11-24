using System;

namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdQuit : ThrottleCmdBase, IThrottleCmd {
		public CmdQuit(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		public string? Execute() {
			Console.WriteLine($"Recieved a QUIT command from '{connectionEntry.ConnectionID}'");
			return null;
		}

		public override string ToString() {
			return "COMMAND: QUIT";
		}
	}
}