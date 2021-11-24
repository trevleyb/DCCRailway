using System;

namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdUnknown : ThrottleCmdBase, IThrottleCmd {
		public CmdUnknown(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		public string? Execute() {
			Console.WriteLine($"Recieved an unknown command from a throttle: '{connectionEntry.ConnectionID}' of '{cmdString}'");
			return null;
		}

		public override string ToString() {
			return "COMMAND: UNKNOWN";
		}
	}
}