using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHardware (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {

    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        if (commandStr.Length > 2) {
            switch (commandStr[1]) {
            case 'U':
                var hardwareID = commandStr[2..];
                if (connection.HasDuplicateID(hardwareID)) {
                    Logger.Log.Debug("CmdFactory [{0}]: Duplicate HardwareIDs found - removing old ones", connection.ConnectionID);
                    connection.RemoveDuplicateID(hardwareID);
                }
                connection.HardwareID = hardwareID;
                Logger.Log.Debug("CmdFactory [{0}]: Set the hardwareID to '{1}'", connection.ConnectionID, hardwareID);
                connection.QueueMsg(new MsgHardware(connection));
                break;
            default:
                break;
            }
        }
    }
    public override string ToString() => "CMD:Hardware";
}