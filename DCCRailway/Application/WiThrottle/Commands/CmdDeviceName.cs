using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdDeviceName : ThrottleCmd, IThrottleCmd {
    public CmdDeviceName(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    // If we get a HardwareID just store it against the entry 
    // Return *xx where xx is the seconds expected between heartbeats
    // -----------------------------------------------------------------------
    public string? Execute() {
        Logger.Log.Information($"Received a THROTTLE NAME command from '{ConnectionEntry.ConnectionID}' of '{CmdString}'");
        ConnectionEntry.ThrottleName = CmdString;

        // Get all the Startup Data needed and return that as a response to a Throttle name
        // ---------------------------------------------------------------------------------
        var startup = new CmdStartup(ConnectionEntry, "", ref Options);
        return startup.Execute();
    }

    public override string ToString() => "COMMAND: THROTTLE NAME";
}