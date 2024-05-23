using System.Text;

namespace DCCRailway.WiThrottle.Messages;

public class MsgTurnoutLabels(Connection connection) : ThrottleMsg, IThrottleMsg
{
    public override string Message
    {
        get
        {
            var turnouts = connection.RailwaySettings.Turnouts.GetAll();
            if (!turnouts.Any()) return "";

            // This block should be re-written in the future to support the Names of the States
            // of the Turnouts to come from Configuration.
            var message = new StringBuilder();
            message.Append("PTT");
            message.Append("]\\[");
            message.Append(connection.RailwaySettings.Turnouts.TurnoutsLabel ?? "Turnouts");
            message.Append("}|{");
            message.Append(connection.RailwaySettings.Turnouts.TurnoutLabel ?? "Turnout");
            message.Append("]\\[");
            message.Append(connection.RailwaySettings.Turnouts.StraightLabel ?? "Closed");
            message.Append("}|{");
            message.Append("2");
            message.Append("]\\[");
            message.Append(connection.RailwaySettings.Turnouts.DivergingLabel ?? "Thrown");
            message.Append("}|{");
            message.Append("4");
            message.AppendLine();
            return message.ToString();
        }
    }

    public override string ToString() => $"MSG:TurnoutLabels [{connection?.ToString() ?? ""}]";
}

/*
 PTT]\[Turnouts}|{Turnout]\[Closed}|{2]\[Thrown}|{4
 PTL]\[LT304}|{Yard Entry}|{2]\[LT305}|{A/D Track}|{4

State is represented by 1 (unknown), 2 (closed), or 4 (thrown).
*/