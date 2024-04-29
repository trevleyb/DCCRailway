using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdDeviceID : ThrottleCmd, IThrottleCmd {
    public CmdDeviceID(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) => connection.LastCommand = this;

    // If we get a HardwareID just store it against the entry and retun a null
    // as we will not respond to the client
    // -----------------------------------------------------------------------
    public IServerMsg Execute() {
        Logger.Log.Information($"Received a HARDWARE ID command from '{Connection.ConnectionID}' of '{CmdString}'");
        Connection.HardwareID = CmdString;

        // Check if we already have an entry for this Hardware ID (in case of a disconnection)
        // and if so, re-map the ID and delete the current one. 
        // ------------------------------------------------------------------------------------
        var existingEntry = Connection.ListReference.Find(CmdString);

        if (existingEntry != null) {
            Logger.Log.Information($"Existing HardwareID reference exists. Remapping '{Connection.ConnectionID}' to '{existingEntry.ConnectionID}'");
            Connection.HardwareID       = existingEntry.HardwareID;
            Connection.HeartbeatSeconds = existingEntry.HeartbeatSeconds;
            Connection.HeartbeatState   = existingEntry.HeartbeatState;
            Connection.LastCommand      = existingEntry.LastCommand;
            Connection.LastHeartbeat    = DateTime.Now;
            Connection.ThrottleName     = existingEntry.ThrottleName;
            Connection.ListReference.Delete(existingEntry);
        }
        return new MsgComplete();
    }

    public override string ToString() => "COMMAND: DEVICE ID";
}