using System.Text;
using DCCRailway.Railway.Throttles.WiThrottle.Helpers;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Railway.Throttles.WiThrottle.Messages;

public class MsgHardware(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var message = new StringBuilder();
            message.Append("HT");
            message.Append(connection.CommandStationManager.CommandStation.AttributeInfo().Name);
            message.Append(Terminators.Terminator);
            message.Append("Ht");
            message.Append(connection.CommandStationManager.CommandStation.Adapter?.AttributeInfo().Name);
            message.Append(" ");
            message.Append(connection.RailwayManager.Name);
            message.AppendLine();
            return message.ToString();
        }
    }
    public override string ToString() => $"MSG:Hardware [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}

/*
 sendPacketToDevice("HtJMRI " + jmri.Version.getCanonicalVersion() + " " + railroadName);
 */