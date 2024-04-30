using System.Text;
using DCCRailway.Application.WiThrottle.Helpers;

namespace DCCRailway.Application.WiThrottle.Messages;


public class MsgRouteLabels(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message {
        get {
            var routes = options?.Config?.Routes.Values;
            if (routes == null || !routes.Any()) return "";

            var message = new StringBuilder();
            message.Append($"PRT");
            message.Append("]\\[");
            message.Append("Routes");
            message.Append("}|{");
            message.Append("Route");
            message.Append("]\\[");
            message.Append("Active");
            message.Append("}|{");
            message.Append("2");
            message.Append("]\\[");
            message.Append("Inactive");
            message.Append("}|{");
            message.Append("4");
            message.Append(Terminators.Terminator);
            return message.ToString();
        }
    }    public override string ToString() => $"MSG:RouteLabels=>{NoTerminators(Message)}";
}

/*
R — Routes
   PRT]\[Routes}|{Route]\[Active}|{2]\[Inactive}|{4
   PRL]\[IO:AUTO:0002}|{CH: Barge 1}|{2]\[IO:AUTO:0003}|{CH: Barge 2}|{4
   */