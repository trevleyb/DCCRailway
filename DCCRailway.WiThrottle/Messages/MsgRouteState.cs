using System.Text;
using DCCRailway.Layout.Entities;

namespace DCCRailway.WiThrottle.Messages;

public class MsgRouteState(Connection connection, Route? route) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            if (route != null) {
                var stateCode = route.State switch {
                    RouteState.Active   => '2',
                    RouteState.Inactive => '4',
                    _                   => '1'
                };
                sb.AppendLine($"PRA{stateCode}{route.Id}");
            }

            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:RouteState [{connection?.ToString() ?? ""}]";
    }
}