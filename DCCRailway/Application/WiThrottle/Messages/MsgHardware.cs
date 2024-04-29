using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgHardware(WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleMsg(connection, options), IThrottleMsg {
    public string Message {
        get {
            var message = new StringBuilder();
            message.Append("HT");
            message.Append("NCE"); // TODO : Get this from the Active Configuration State
            message.Append(Terminators.Terminator);
            message.Append("Ht");
            message.Append("USB"); // TODO : Get this from the Active Configuration State
            message.Append(Terminators.Terminator);
            return message.ToString();
        }
    }
    public override string ToString() => $"MSG:Hardware=>{NoTerminators(Message)}";
}