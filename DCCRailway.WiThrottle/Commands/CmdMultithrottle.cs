using DCCRailway.Common.Types;
using DCCRailway.WiThrottle.Helpers;
using DCCRailway.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public partial class CmdMultiThrottle(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd from [{0}]: Multithrottle - {1}:{3}=>'{2}'", connection.ConnectionHandle, ToString(), commandStr, connection.ToString());

        try {
            IThrottleMsg[]? response = null;
            var             data     = new MultiThrottleMessage(connection, commandStr);
            if (!data.IsValid) return;

            // Process the data based on the Command Function (first 3 characters)
            // ------------------------------------------------------------------------------------------------------
            // Logger.Log.Information("{0}=>'{1}' Split into: '{2}'.'{3}' => '{4}'", ToString(), commandStr, data.Function, data.Address, data.Action);

            response = data.Function switch {
                ThrottleFunctionEnum.AcquireLoco          => RequestLocoAccess(data),
                ThrottleFunctionEnum.ReleaseLoco          => ReleaseLocoAccess(data),
                ThrottleFunctionEnum.StealLoco            => StealLocoAddress(data),
                ThrottleFunctionEnum.ProvideLocoFunctions => ProvideLocoFunctions(data),
                ThrottleFunctionEnum.PerformLocoAction    => PerformLocoAction(data),
                _                                         => null
            };

            if (response is not null) connection.QueueMsg(response);
        } catch {
            logger.Error("WiThrottle Recieved Cmd: Multithrottle - {0}:{2}=> Unable to Process the command =>'{1}'", ToString(), commandStr, connection.ToString());
        }
    }

    /// <summary>
    /// Acquire a Loco and assign it to this WiThrottle Only
    /// Return a STEAL command if it is assigned to another loco
    /// </summary>
    private IThrottleMsg[] RequestLocoAccess(MultiThrottleMessage data) {
        var responses = new List<IThrottleMsg>();

        // If the loco is already assigned, then we need to refuse the connection
        foreach (var address in data.Addresses) {
            if (connection.IsAddressInUse(address)) {
                logger.Information("WiThrottle Recieved Cmd: Request for loco: {0} refused as in use. ", address.Address.ToString());
                responses.Add(new MsgAddressRefused(connection, address, data.Group, (char)data.Function));
            } else {
                logger.Information("WiThrottle Recieved Cmd: Acquiring loco: {0} ", address.Address.ToString());
                connection.Acquire(data.Group, address);
                var locoData = connection.GetLoco(address);
                if (locoData is not null) {
                    locoData.Speed     = new DCCSpeed(0);
                    locoData.Direction = DCCDirection.Forward;
                }

                responses.Add(new MsgAddress(connection, address, data.Group, (char)data.Function));
                responses.Add(new MsgLocoLabels(connection, address, data.Group, (char)data.Function));
                responses.Add(new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'V', "0"));
                responses.Add(new MsgQueryValue(connection, address, data.Group, (char)data.Function, 'R', "1"));
            }
        }

        return responses.ToArray();
    }

    /// <summary>
    /// Release a loco from the collection of held Locos and tell WiThrottle it has been released
    /// </summary>
    private IThrottleMsg[] ReleaseLocoAccess(MultiThrottleMessage data) {
        var responses = new List<IThrottleMsg>();
        foreach (var address in data.Addresses) {
            logger.Information("WiThrottle Recieved Cmd: Releasing loco: {0}", address.ToString());
            var owner = connection.Release(address);
            if (owner is not null && owner != connection) {
                logger.Information("WiThrottle Recieved Cmd: Releasing loco: {0} but owner is different. ", address.ToString());
                owner.QueueMsg(new MsgAddressReleased(owner, address, data.Group, (char)data.Function));
            }

            responses.Add(new MsgAddressReleased(connection, address, data.Group, (char)data.Function));
        }

        return responses.ToArray();
    }

    /// <summary>
    /// Steam a Loco. This is issued if it is assigned elsewhere but this throttle wants it.
    /// We need to release it and let the other connection know they no longer have access
    /// to it, and then assign it to this loco.
    /// </summary>
    private IThrottleMsg[] StealLocoAddress(MultiThrottleMessage data) {
        var responses = new List<IThrottleMsg>();
        foreach (var address in data.Addresses) {
            if (!connection.IsAddressInUse(address)) {
                return RequestLocoAccess(data);
            } else {
                logger.Information("WiThrottle Recieved Cmd: Stealing loco: {0}", address.ToString());
                var owner = connection.Release(address);

                if (owner is not null && owner != connection) {
                    logger.Information("WiThrottle Recieved Cmd: Releasing stolen loco: {0} from different owner. ", address.ToString());
                    owner?.QueueMsg(new MsgAddressReleased(owner, address, data.Group, (char)data.Function));
                }

                responses.Add(new MsgAddress(connection, address, data.Group, (char)data.Function));
            }
        }

        return responses.ToArray();
    }

    private IThrottleMsg[] ProvideLocoFunctions(MultiThrottleMessage data) {
        var responses = new List<IThrottleMsg>();
        foreach (var address in data.Addresses) {
            responses.Add(new MsgLocoLabels(connection, address, data.Group, (char)data.Function));
        }

        return responses.ToArray();
    }
}