using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdDeviceName : ThrottleCmd, IThrottleCmd {
    public CmdDeviceName(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) => connection.LastCommand = this;

    // If we get a HardwareID just store it against the entry 
    // Return *xx where xx is the seconds expected between heartbeats
    // -----------------------------------------------------------------------
    public IServerMsg Execute() {
        Logger.Log.Information($"Received a THROTTLE NAME command from '{Connection.ConnectionID}' of '{CmdString}'");
        Connection.ThrottleName = CmdString;
        return new MsgStartup(Connection, ref Options);
    }

    public override string ToString() => "COMMAND: THROTTLE NAME";
}