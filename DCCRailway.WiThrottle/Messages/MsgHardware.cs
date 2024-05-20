using System.Text;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgHardware(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var message = new StringBuilder();
            message.Append("HT");
            //TODO: message.Append(connection.CommandStationManager.CommandStation.AttributeInfo().Name);
            message.Append(Terminators.Terminator);
            message.Append("Ht");
            //TODO: message.Append(connection.CommandStationManager.CommandStation.Adapter?.AttributeInfo().Name);
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