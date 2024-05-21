using System.Net.Sockets;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Controllers;
using DCCRailway.Layout;

namespace DCCRailway.WiThrottle;

/// <summary>
///     ConnectionInfo stores the details of each connected throttle to the commandStation
///     and will track a connection to a Command Station and what commands are or
///     have been sent on behalf of the throttle.
/// </summary>
public class WiThrottleConnections {

    private readonly List<WiThrottleConnection> _connections = new List<WiThrottleConnection>();
    public int Count => _connections.Count;
    /// <summary>
    ///     Add a new entry into the list of connected Throttles
    /// </summary>
    public WiThrottleConnection Add(TcpClient client, IRailwaySettings railwaySettings, ICommandStation commandStation) {
        var connection = new WiThrottleConnection(client, railwaySettings, this, commandStation);
        _connections.Add(connection);
        return connection;
    }

    public WiThrottleConnection? GetByHardwareID(string hardwareID,ulong connectionHandle) {
        return _connections.FirstOrDefault(x => x.HardwareID.Equals(hardwareID) && x.ConnectionHandle != connectionHandle);
    }

    public void RemoveDuplicateID(string hardwareID, ulong connectionHandle) {
        for (var i = _connections.Count - 1; i >= 0; i--) {
            if (_connections[i].HardwareID.Equals(hardwareID) &&
                _connections[i].ConnectionHandle != connectionHandle)
                _connections.RemoveAt(i);
        }
    }

    public bool HasDuplicateID(string hardwareID,ulong connectionHandle) {
        return _connections.Any(x => x.HardwareID.Equals(hardwareID) && x.ConnectionHandle != connectionHandle);
    }

    public void CloseConnectionsWithCondition(Func<WiThrottleConnection, bool> conditionToClose, string logMessage) {
        var connectionsToClose = _connections.Where(conditionToClose).ToList();
        foreach (var connection in connectionsToClose) {
            connection.Close();
        }
    }

    public bool IsAddressInUse(DCCAddress address) {
        foreach (var connection in _connections) {
            if (connection.IsLocoAssigned(address)) return true;
        }
        return false;
    }

    public void Release(DCCAddress address) {
        foreach (var connection in _connections) {
            if (connection.IsLocoAssigned(address)) connection.Release(address);
        }
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    public void Remove(WiThrottleConnection entry) {
        _connections.Remove(entry);
    }

    /// <summary>
    ///     Find an entry in the list by the UniqueID of the Throttle
    /// </summary>
    /// <param name="hardwareID">The unique hardware ID of the throttle</param>
    /// <returns>A entry of a connected throttle</returns>
    public WiThrottleConnection? Find(string hardwareID) => _connections.FirstOrDefault(x => x!.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));

    //public WiThrottleConnection? Find(ulong connectionID) => _connections.FirstOrDefault(x => x.ConnectionHandle == connectionID);
}