using System.Text;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;


public class MsgTurnoutList(WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleMsg(connection, options), IThrottleMsg {
    public string Message {
        get {
            var turnouts = options?.Config?.Turnouts.Values;
            if (turnouts is null || !turnouts.Any()) return "";

            // This block should be re-written in the future to support the Names of the States
            // of the Turnouts to come from Condfiguration.
            var message = new StringBuilder();
            message.Append($"PTL");
            foreach (var turnout in turnouts) {
                message.Append("]\\["); // Separator
                message.Append(turnout.Id);
                message.Append("}|{");
                message.Append(turnout.Name.RemoveWiThrottleSeparators());
                message.Append("}|{");
                message.Append(turnout.CurrentState == DCCTurnoutState.Closed ? "2" :
                               turnout.CurrentState == DCCTurnoutState.Thrown ? "4" : "1");
            }
            message.Append(Terminators.Terminator);
            return message.ToString();
        }
    }    public override string ToString() => $"MSG:TurnoutList=>{NoTerminators(Message)}";
}

/*
 PTT]\[Turnouts}|{Turnout]\[Closed}|{2]\[Thrown}|{4
 PTL]\[LT304}|{Yard Entry}|{2]\[LT305}|{A/D Track}|{4

State is represented by 1 (unknown), 2 (closed), or 4 (thrown).
*/