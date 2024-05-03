using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Station.Attributes;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgHardware(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var message = new StringBuilder();
            message.Append("HT");
            message.Append(connection.ActiveController.AttributeInfo().Name);
            message.Append(Terminators.Terminator);
            message.Append("Ht");
            message.Append(connection.ActiveController.Adapter?.AttributeInfo().Name);
            message.Append(" ");
            message.Append(connection.RailwayConfig.Name);
            message.AppendLine();
            return message.ToString();
        }
    }
    public override string ToString() => $"MSG:Hardware [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}

/*
 sendPacketToDevice("HtJMRI " + jmri.Version.getCanonicalVersion() + " " + railroadName);
 */