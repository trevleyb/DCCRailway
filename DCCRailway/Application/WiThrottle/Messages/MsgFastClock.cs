using System.Text;

namespace DCCRailway.Application.WiThrottle.Messages;


public class MsgFastClock(WiThrottleConnection connection, FastClockState state, DateTime clock, int ratio) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var timeSince1970 = (new DateTime(1970, 1, 1, 0, 0, 0) - clock).Seconds;
            var sb = new StringBuilder();
            sb.Append("PFT");
            sb.Append(timeSince1970);
            sb.Append("<;>");
            sb.AppendLine(state == FastClockState.Stop ? $"0.0" : $"{ratio}.0");
            return sb.ToString();

            /*
             * T - Current time to display. Format is PFT followed by the number of seconds from the
               reference date of 00:00:00 UTC on 1 January 1970. It is preferred to append <;> rate
               value - but not mandatory. Adding the rate to the string allows self-running clocks and
               reduces the frequency that time updates should be sent. If no rate is sent, clocks will
               update only by receiving new time value. Sent only to client.
               PFT1607717340<;>1.0
               PFT74100<;>1.0
               Start clock running at 1.0:1 rate at 20:09:00.
               PFT1607718869<;>0.0
               Stop clock.
               PFT1607718910<;>4.0
               Restart clock with rate of 4.0:1 at 20:35:10.
             */
        }
    }
}

public enum FastClockState {
    Start,
    Stop,
    Reset
}