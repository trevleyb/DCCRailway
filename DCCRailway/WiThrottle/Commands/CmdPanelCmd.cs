namespace DCCRailway.Station.WiThrottle.Commands;

public class CmdPanelCmd : ThrottleCmdBase, IThrottleCmd {
    public CmdPanelCmd(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) => connectionEntry.LastCommand = this;

    public string? Execute() => null;

    public override string ToString() => "COMMAND: PANEL COMMAND";
}