using System;

namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdDeviceID : ThrottleCmdBase, IThrottleCmd {
		public CmdDeviceID(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		// If we get a HardwareID just store it against the entry and retun a null
		// as we will not respond to the client
		// -----------------------------------------------------------------------
		public string? Execute() {
			Console.WriteLine($"Recieved a HARDWARE ID command from '{connectionEntry.ConnectionID}' of '{cmdString}'");
			connectionEntry.HardwareID = cmdString;

			// Check if we already have an entry for this Hardware ID (in case of a disconnection)
			// and if so, re-map the ID and delete the current one. 
			// ------------------------------------------------------------------------------------
			var existingEntry = connectionEntry.listReference.Find(cmdString);
			if (existingEntry != null) {
				Console.WriteLine($"Existing HardwareID reference exists. Remapping '{connectionEntry.ConnectionID}' to '{existingEntry.ConnectionID}'");
				connectionEntry.HardwareID = existingEntry.HardwareID;
				connectionEntry.HeartbeatSeconds = existingEntry.HeartbeatSeconds;
				connectionEntry.HeartbeatState = existingEntry.HeartbeatState;
				connectionEntry.LastCommand = existingEntry.LastCommand;
				connectionEntry.LastHeartbeat = DateTime.Now;
				connectionEntry.ThrottleName = existingEntry.ThrottleName;
				connectionEntry.listReference.Delete(existingEntry);
			}

			return null;
		}

		public override string ToString() {
			return "COMMAND: DEVICE ID";
		}
	}
}