using System.Text;

namespace DCCRailway.WiThrottle.Messages;

public class MsgRouteLabels(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var routes = connection.RailwaySettings.Routes.GetAll();
            if (!routes.Any()) return "";

            var message = new StringBuilder();
            message.Append("PRT");
            message.Append("]\\[");
            message.Append(connection.RailwaySettings.Routes.RoutesLabel ?? "Routes");
            message.Append("}|{");
            message.Append(connection.RailwaySettings.Routes.RouteLabel ?? "Route");
            message.Append("]\\[");
            message.Append(connection.RailwaySettings.Routes.ActiveLabel ?? "Active");
            message.Append("}|{");
            message.Append("2");
            message.Append("]\\[");
            message.Append(connection.RailwaySettings.Routes.InActiveLabel ?? "Inactive");
            message.Append("}|{");
            message.Append("4");
            message.AppendLine();
            return message.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:RouteLabels [{connection?.ToString() ?? ""}]";
    }
}

/*
R — Routes
   PRT]\[Routes}|{Route]\[Active}|{2]\[Inactive}|{4
   PRL]\[IO:AUTO:0002}|{CH: Barge 1}|{2]\[IO:AUTO:0003}|{CH: Barge 2}|{4
   */