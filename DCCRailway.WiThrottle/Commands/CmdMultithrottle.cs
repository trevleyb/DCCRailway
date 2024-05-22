using DCCRailway.Common.Types;
using DCCRailway.WiThrottle.Helpers;
using DCCRailway.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdMultiThrottle(ILogger logger, WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Cmd: Multithrottle - {0}:{2}=>'{1}'", ToString(), commandStr, connection.ToString());
        try {
            IThrottleMsg[]? response = null;
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
        } catch {
            logger.Error("WiThrottle Cmd: Multithrottle - {0}:{2}=> Unable to Process the command =>'{1}'", ToString(), commandStr, connection.ToString());
        }
    }

    /// <summary>
    /// Acquire a Loco and assign it to this WiThrottle Only
    /// Return a STEAL command if it is assigned to another loco
    /// </summary>
    private IThrottleMsg[] RequestLocoAccess(MultiThrottleMessage data) {
        var responses = new List<IThrottleMsg>();

        // If the loco is already assigned, then we need to refuse the connection
        if (connection.IsAddressInUse(data.Address)) {
            logger.Information("WiThrottle Cmd: Request for loco: {0} refused as in use. ", data.Address.ToString());
            responses.Add(new MsgAddressRefused(connection, data));
        } else {
            logger.Information("WiThrottle Cmd: Acquiring loco: {0} ", data.Address.ToString());
            connection.Acquire(data.Group, data.Address);
            responses.Add(new MsgAddress(connection, data));
            responses.Add(new MsgLocoLabels(connection, data));
        }
        return responses.ToArray();
    }

    /// <summary>
    /// Release a loco from the collection of held Locos and tell WiThrottle it has been released
    /// </summary>
    private IThrottleMsg[] ReleaseLocoAccess(MultiThrottleMessage data) {
        var responses = new List<IThrottleMsg>();
        logger.Information("WiThrottle Cmd: Releasing loco: {0}", data.Address.ToString());

        // If we get a Broadcast message (ie: it was a *) then we need to release ALL
        // locos that are in the same group for the same connection (if any)
        // -------------------------------------------------------------------------
        if (data.Address.AddressType == DCCAddressType.Broadcast) {
            var locosToRelease = connection.ReleaseAllInGroup(data.Group);
            foreach (var loco in locosToRelease) {
                logger.Information("WiThrottle Cmd: Releasing loco: {0} from group: {1}", loco.ToString(), data.Group);
                responses.Add(new MsgAddress(connection,data,loco));
            }
        } else {
            var owner = connection.Release(data.Address);
            if (owner is not null && owner != connection) {
                logger.Information("WiThrottle Cmd: Releasing loco: {0} but owner is different. ", data.Address.ToString());
                owner.QueueMsg(new MsgAddressReleased(owner, data));
            }
            responses.Add(new MsgAddressReleased(connection, data));
        }
        return responses.ToArray();
    }

    /// <summary>
    /// Steam a Loco. This is issued if it is assigned elsewhere but this throttle wants it.
    /// We need to release it and let the other connection know they no longer have access
    /// to it, and then assign it to this loco.
    /// </summary>
    private IThrottleMsg[] StealLocoAddress(MultiThrottleMessage data) {
        if (!connection.IsAddressInUse(data.Address)) return RequestLocoAccess(data);
        else {
            logger.Information("WiThrottle Cmd: Stealing loco: {0}", data.Address.ToString());
            var owner = connection.Release(data.Address);
            if (owner is not null && owner != connection) {
                logger.Information("WiThrottle Cmd: Releasing stolen loco: {0} from different owner. ", data.Address.ToString());
                owner?.QueueMsg(new MsgAddressReleased(owner,data));
            }
            return [new MsgAddress(connection, data)];
        }
    }

    private IThrottleMsg[] PerformLocoAction(MultiThrottleMessage data) => [new MsgAddress(connection, data)];
    private IThrottleMsg[] ProvideLocoFunctions(MultiThrottleMessage data) => [new MsgLocoLabels(connection, data)];
    public override string ToString() => "CMD:MultiThrottle";
}