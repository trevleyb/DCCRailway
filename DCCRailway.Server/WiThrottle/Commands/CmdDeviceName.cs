using DCCRailway.Core.Utilities;

namespace DCCRailway.Server.WiThrottle.Commands;

public class CmdDeviceName : ThrottleCmdBase, IThrottleCmd {
    public CmdDeviceName(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
        connectionEntry.LastCommand = this;
    }

    // If we get a HardwareID just store it against the entry 
    // Return *xx where xx is the seconds expected between heartbeats
    // -----------------------------------------------------------------------
    public string? Execute() {
        Logger.Log.Information($"Received a THROTTLE NAME command from '{ConnectionEntry.ConnectionID}' of '{CmdString}'");
        ConnectionEntry.ThrottleName = CmdString;

        // Get all the Startup Data needed and return that as a response to a Throttle name
        // ---------------------------------------------------------------------------------
        var startup = new CmdStartup(ConnectionEntry, "");

        return startup.Execute();
    }

    public override string ToString() {
        return "COMMAND: THROTTLE NAME";
    }
}