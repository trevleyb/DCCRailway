namespace DCCRailway.Server.WiThrottle.Commands {
	public abstract class ThrottleCmdBase {
		protected string cmdString;

		protected WiThrottleConnectionEntry connectionEntry;

		public ThrottleCmdBase(WiThrottleConnectionEntry connectionEntry, string cmdString) {
			this.connectionEntry = connectionEntry;
			this.cmdString = cmdString;
			this.connectionEntry.UpdateHeartbeat();
		}
	}
}