using System.Text;
using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Messages;

public class MsgRouteList(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var routes = connection.RailwayManager.Routes.GetAll();
            if (!routes.Any()) return "";

            var message = new StringBuilder();
            message.Append("PRL");
            foreach (var route in routes) {
                message.Append("]\\["); // Separator
                message.Append(route.Id);
                message.Append("}|{");
                message.Append(route.Name.RemoveWiThrottleSeparators());
                message.Append("}|{");
                message.Append("1");

                //route.CurrentState == DCCTurnoutState.Closed ? "2" :
                //route.CurrentState == DCCTurnoutState.Thrown ? "4" : "1");
            }
            message.AppendLine();
            return message.ToString();
        }
    }

    public override string ToString() => $"MSG:RouteList [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}

/*

R — Routes
   PRT]\[Routes}|{Route]\[Active}|{2]\[Inactive}|{4
   PRL]\[IO:AUTO:0002}|{CH: Barge 1}|{2]\[IO:AUTO:0003}|{CH: Barge 2}|{4
   */