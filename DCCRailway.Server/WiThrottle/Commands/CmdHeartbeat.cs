using System;

namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdHeartBeat : ThrottleCmdBase, IThrottleCmd {
		public CmdHeartBeat(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		public string? Execute() {
			if (cmdString.Equals("+")) {
				Console.WriteLine($"Recieved a HEARTBEAT + (ON) command from '{connectionEntry.ThrottleName}'");
				connectionEntry.HeartbeatState = HeartbeatStateEnum.On;
			} else if (cmdString.Equals("-")) {
				Console.WriteLine($"Recieved a HEARTBEAT - (OFF) command from '{connectionEntry.ThrottleName}'");
				connectionEntry.HeartbeatState = HeartbeatStateEnum.Off;
			} else {
				Console.WriteLine($"Recieved a HEARTBEAT from '{connectionEntry.ThrottleName}'");

				// Do nothing as we jyust recieved a heartbeat
			}

			return null;
		}

		public override string ToString() {
			return "COMMAND: HEARTBEAT";
		}
	}
}