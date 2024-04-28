namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdPanelCmd : ThrottleCmd, IThrottleCmd {
    public CmdPanelCmd(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    public string? Execute() => null;

    public override string ToString() => "COMMAND: PANEL COMMAND";
}