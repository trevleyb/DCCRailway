using System.Text;
using DCCRailway.Controller.Attributes;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgHardware(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var message = new StringBuilder();
            message.Append("HT");
            message.Append(connection.CommandStation.AttributeInfo().Name);
            message.Append(Terminators.Terminator);
            message.Append("Ht");
            message.Append(connection.CommandStation.Adapter?.AttributeInfo().Name);
            message.Append(" ");
            message.Append(connection.RailwaySettings.Name);
            message.AppendLine();
            return message.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:Hardware [{connection?.ToString() ?? ""}]";
    }
}

/*
 sendPacketToDevice("HtJMRI " + jmri.Version.getCanonicalVersion() + " " + railroadName);
 */