using System.Text;

namespace DCCRailway.Station.WiThrottle.Commands;

/// <summary>
///     Startup command used to setup a new connection for a Throttle
/// </summary>
public class CmdStartup : ThrottleCmdBase, IThrottleCmd {
    public CmdStartup(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) { }

    public string? Execute() {
        StringBuilder response = new();
        response.AppendLine("VN2.0");
        response.AppendLine("RL0");  // TODO: Add roster entries
        response.AppendLine("PPA0"); // TODO: Add Power Support

        //response.AppendLine ("PTT0");		// TODO: Add Turnouts
        //response.AppendLine ("PRT0");		// TODO: Add Routes
        //response.AppendLine ("PCC0");		// TODO: Add Consists
        response.AppendLine("PW12080");
        response.AppendLine($"*{ConnectionEntry.HeartbeatSeconds:D2}");

        return response.ToString();
    }

    public override string ToString() => "COMMAND: STARTUP";
}