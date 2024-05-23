using System.Text;
using DCCRailway.Layout.Entities;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgLocoLabels(Connection connection, MultiThrottleMessage data) : ThrottleMsg, IThrottleMsg
{
    private const byte maxLabels = 29;

    public override string Message
    {
        get
        {
            var sb = new StringBuilder();
            sb.Append($"M{data.Group}L");
            sb.Append(data.Address.IsLong ? "L" : "S");
            sb.Append(data.Address.Address);
            sb.Append("<;>");
            var loco = connection.RailwaySettings.Locomotives.Find(x => x.Address.Address == data.Address.Address);
            if (loco is not null) sb.Append(GenerateLabels(loco));
            sb.AppendLine();
            return sb.ToString();
        }
    }

    private string GenerateLabels(Locomotive loco)
    {
        // Build up the labels for the Locomotive. There should always be 29
        // items in the list, but most will be blank using ]/[
        // ----------------------------------------------------------------------------------
        var max = Math.Min(loco.Labels.Max(x => x.Key), maxLabels);
        var sb  = new StringBuilder();
        for (byte num = 0; num < max; num++)
        {
            sb.Append("]\\[");
            var label = loco.Labels.Find(x => x.Key == num)?.Label ?? "";
            sb.Append(label);
        }

        return sb.ToString();
    }

    public override string ToString() => $"MSG:MSGAddress [{connection?.ToString() ?? ""}]";
}