using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Conversion.JMRI;
using DCCRailway.Station.Commands.Types;
using Microsoft.VisualBasic;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdMultiThrottle(WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}:{2}=>'{1}'", ToString(), commandStr,connection.ToString());
        try {
            IThrottleMsg? response = null;
            var data = new MultiThrottleMessage(commandStr);
            if (!data.IsValid) return;

            // Process the data based on the Command Function (first 3 characters)
            // ------------------------------------------------------------------------------------------------------
            // Logger.Log.Information("{0}=>'{1}' Split into: '{2}'.'{3}' => '{4}'", ToString(), commandStr, data.Function, data.Address, data.Action);

            response = data.Function switch {
                '+' => RequestLocoAccess(data),
                '-' => ReleaseLocoAccess(data),
                'S' => StealLocoAddress(data),
                'L' => ProvideLocoFunctions(data),
                'A' => PerformLocoAction(data),
                _   => null
            };
            if (response is not null) connection.QueueMsg(response);
        }
        catch {
            Logger.Log.Error("{0}:{2}=> Unable to Process the command =>'{1}'", ToString(), commandStr,connection.ToString());
        }
    }

    private IThrottleMsg? RequestLocoAccess(MultiThrottleMessage data) {
        return new MsgAddress(connection,data);
    }

    private IThrottleMsg? ReleaseLocoAccess(MultiThrottleMessage data) {
        return new MsgAddress(connection,data);
    }

    private IThrottleMsg? StealLocoAddress(MultiThrottleMessage data) {
        return new MsgAddress(connection,data);
    }

    private IThrottleMsg? PerformLocoAction(MultiThrottleMessage data) {
        return new MsgAddress(connection,data);
    }

    private IThrottleMsg? ProvideLocoFunctions(MultiThrottleMessage data) {
        return new MsgAddress(connection,data);
    }

    public override string ToString() => $"CMD:MultiThrottle";

}