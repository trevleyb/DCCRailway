using System.Text;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Components;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Repository.Base;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Commands;

/// <summary>
///     Startup command used to setup a new connection for a Throttle
/// </summary>
public class CmdStartup : ThrottleCmd, IThrottleCmd {
    public CmdStartup(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) { }

    public IServerMsg Execute() {
        return new MsgStartup(Connection,ref Options);
    }

    public override string ToString() => "COMMAND: STARTUP";
}