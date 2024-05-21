using System.Text;
using DCCRailway.Layout.Entities;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgLabels(WiThrottleConnection connection, MultiThrottleMessage data) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append(new MsgAddress(connection, data));
            var loco = connection.RailwaySettings.Locomotives.Find(x => x.Address.Address == data.Address.Address);
            if (loco is not null) sb.Append(GenerateLabels(loco));
            return sb.ToString();
        }
    }

    private string GenerateLabels(Locomotive loco)
    {
        var sb = new StringBuilder();
        var labels = loco.Labels
                         .Where(x => x.Key < 29)
                         .Select(x => x.Label ?? "")
                         .Where(label => !string.IsNullOrEmpty(label));

        foreach(var label in labels) {
            sb.Append("]/[");
            sb.Append(label);
        }
        return sb.ToString();
    }

    public override string ToString() => $"MSG:MSGAddress [{connection?.ToString() ?? ""}]";
}