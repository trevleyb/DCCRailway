using DCCRailway.Common.Types;
using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgPanel(ILogger logger, Turnouts turnouts) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Panel {0}:=>'{1}'", ToString(), commandStr);
        switch (commandStr[1..3]) {
        case "PTL":
            // eg: PTL]\[LT304}|{Yard Entry}|{2]\[LT305}|{A/D Track}|{4
            try {
                var turnoutsStr = commandStr[4..];
                var turnoutsArr = turnoutsStr.Split("]\\[");
                foreach (var turnout in turnoutsArr) {
                    var turnoutArr = turnout.Split("|");
                    var systemName = turnoutArr[0];
                    var userName   = turnoutArr[1];
                    var state      = turnoutArr[2];
                    turnouts.Add(systemName, userName, state);
                }
            } catch { /* ignore any issues */
            }

            break;
        case "PTA":
            // EG: PTATLT304
            var thrownOrClosed = commandStr[3];
            var turnoutName    = commandStr[4..];
            var turnoutEntry   = turnouts.Find(turnoutName);
            turnoutEntry.State = thrownOrClosed switch {
                'C' => DCCTurnoutState.Closed,
                'T' => DCCTurnoutState.Thrown,
                _   => DCCTurnoutState.Unknown
            };

            break;
        case "PTT":
            break;
        }
    }

    public override string ToString() {
        return "MSG:Panel";
    }
}

/*
   • L - Turnout list with a format of ]\[ system name }|{ user name }|{ state. This
   pattern repeats for each turnout stored in the server that is available to the client.
   PTL]\[LT304}|{Yard Entry}|{2]\[LT305}|{A/D Track}|{4
   • A - Action. Has two patterns:
   1. Sent to server - commands are 2 (toggle), C (closed), or T (thrown).
   PTA, followed by command, followed by system name.
   PTATLT304
   Set “thrown” LT304.
   2. Sent to client - States are 1 (unknown), 2 (closed), or 4 (thrown). Server should
   send state to clients whenever state changes.
   PTA, followed by state, followed by system name.
   PTA4LT304
   State of LT304 is “thrown”.

   */