using System;

namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdDeviceName : ThrottleCmdBase, IThrottleCmd {
		public CmdDeviceName(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		// If we get a HardwareID just store it against the entry 
		// Return *xx where xx is the seconds expected between heartbeats
		// -----------------------------------------------------------------------
		public string? Execute() {
			Console.WriteLine($"Recieved a THROTTLE NAME command from '{connectionEntry.ConnectionID}' of '{cmdString}'");
			connectionEntry.ThrottleName = cmdString;

			// Get all the Startup Data needed and return that as a response to a Throttle name
			// ---------------------------------------------------------------------------------
			var startup = new CmdStartup(connectionEntry, "");
			return startup.Execute();
		}

		public override string ToString() {
			return "COMMAND: THROTTLE NAME";
		}
	}
}