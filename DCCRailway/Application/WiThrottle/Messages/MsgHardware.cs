using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Station.Attributes;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgHardware(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message {
        get {
            var message = new StringBuilder();
            message.Append("HT");
            message.Append(options.Controller?.AttributeInfo().Name);
            message.Append(Terminators.Terminator);
            message.Append("Ht");
            message.Append(options.Controller?.Adapter?.AttributeInfo().Name);
            message.Append(" ");
            message.Append(options.Config?.Name);
            message.Append(Terminators.Terminator);
            return message.ToString();
        }
    }
    public override string ToString() => $"MSG:Hardware=>{NoTerminators(Message)}";
}

/*
 sendPacketToDevice("HtJMRI " + jmri.Version.getCanonicalVersion() + " " + railroadName);
 */