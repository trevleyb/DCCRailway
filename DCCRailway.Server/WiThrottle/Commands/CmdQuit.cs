﻿using System;

namespace DCCRailway.Server.WiThrottle.Commands {
	public class CmdQuit : ThrottleCmdBase, IThrottleCmd {
		public CmdQuit(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
			connectionEntry.LastCommand = this;
		}

		public string? Execute() {
			Core.Utilities.Logger.Log.Information($"Received a QUIT command from '{ConnectionEntry.ConnectionID}'");
			return null;
		}

		public override string ToString() {
			return "COMMAND: QUIT";
		}
	}
}