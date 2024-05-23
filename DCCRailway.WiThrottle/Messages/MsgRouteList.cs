using System.Text;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgRouteList(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var routes = connection.RailwaySettings.Routes.GetAll();
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

    public override string ToString() {
        return $"MSG:RouteList [{connection?.ToString() ?? ""}]";
    }
}

/*

R — Routes
   PRT]\[Routes}|{Route]\[Active}|{2]\[Inactive}|{4
   PRL]\[IO:AUTO:0002}|{CH: Barge 1}|{2]\[IO:AUTO:0003}|{CH: Barge 2}|{4
   */