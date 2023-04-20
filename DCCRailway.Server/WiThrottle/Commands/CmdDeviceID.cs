using System;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Server.WiThrottle.Commands; 

public class CmdDeviceID : ThrottleCmdBase, IThrottleCmd {
    public CmdDeviceID(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
        connectionEntry.LastCommand = this;
    }

    // If we get a HardwareID just store it against the entry and retun a null
    // as we will not respond to the client
    // -----------------------------------------------------------------------
    public string? Execute() {
        Logger.Log.Information($"Received a HARDWARE ID command from '{ConnectionEntry.ConnectionID}' of '{CmdString}'");
        ConnectionEntry.HardwareID = CmdString;

        // Check if we already have an entry for this Hardware ID (in case of a disconnection)
        // and if so, re-map the ID and delete the current one. 
        // ------------------------------------------------------------------------------------
        var existingEntry = ConnectionEntry.listReference.Find(CmdString);
        if (existingEntry != null) {
            Logger.Log.Information($"Existing HardwareID reference exists. Remapping '{ConnectionEntry.ConnectionID}' to '{existingEntry.ConnectionID}'");
            ConnectionEntry.HardwareID = existingEntry.HardwareID;
            ConnectionEntry.HeartbeatSeconds = existingEntry.HeartbeatSeconds;
            ConnectionEntry.HeartbeatState = existingEntry.HeartbeatState;
            ConnectionEntry.LastCommand = existingEntry.LastCommand;
            ConnectionEntry.LastHeartbeat = DateTime.Now;
            ConnectionEntry.ThrottleName = existingEntry.ThrottleName;
            ConnectionEntry.listReference.Delete(existingEntry);
        }

        return null;
    }

    public override string ToString() {
        return "COMMAND: DEVICE ID";
    }
}