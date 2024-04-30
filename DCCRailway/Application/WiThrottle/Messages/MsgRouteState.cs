using System.Text;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgRouteState(WiThrottleServerOptions options, Layout.Configuration.Entities.Layout.Route? route) : ThrottleMsg, IThrottleMsg {
    public string Message {
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
    public override string ToString() => $"MSG:RouteState=>{NoTerminators(Message)}";

}