﻿using DCCRailway.Application.WiThrottle.Messages;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdDirect : ThrottleCmd, IThrottleCmd {
    public CmdDirect(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) => connection.LastCommand = this;

    public IServerMsg Execute() => new MsgComplete();

    public override string ToString() => "COMMAND: SEND DIRECT DATA";
}