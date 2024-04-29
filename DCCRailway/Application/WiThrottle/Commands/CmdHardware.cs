using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHardware (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd(connection, options), IThrottleCmd {

    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        if (commandStr.Length > 2) {
            switch (commandStr[1]) {
            case 'U':
                var hardwareID = commandStr[2..];
                if (Connection.HasDuplicateID(hardwareID)) {
                    Logger.Log.Debug("CmdFactory [{0}]: Duplicate HardwareIDs found - removing old ones", Connection.ConnectionID);
                    Connection.RemoveDuplicateID(hardwareID);
                }
                Connection.HardwareID = hardwareID;
                Logger.Log.Debug("CmdFactory [{0}]: Set the hardwareID to '{1}'", Connection.ConnectionID, hardwareID);
                Connection.AddResponseMsg(new MsgHardware(Connection,Options));
                break;
            default:
                break;
            }
        }
    }
    public override string ToString() => "CMD:Hardware";
}