using System.Data.SqlTypes;
using System.Net.Sockets;
using System.Text.RegularExpressions;
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

    public readonly WiThrottleAssignedLocos Assignments = new();
    public readonly List<WiThrottleConnection> Connections = [];
    public int Count => Connections.Count;

    /// <summary>
    ///     Add a new entry into the list of connected Throttles
    /// </summary>
    public WiThrottleConnection Add(TcpClient client, IRailwaySettings railwaySettings, ICommandStation commandStation) {
        var connection = new WiThrottleConnection(client, railwaySettings, this, commandStation);
        Connections.Add(connection);
        return connection;
    }

    public WiThrottleConnection? GetByHardwareID(string hardwareID,ulong connectionHandle) {
        return Connections.FirstOrDefault(x => x.HardwareID.Equals(hardwareID) && x.ConnectionHandle != connectionHandle);
    }

    public void RemoveDuplicateID(string hardwareID, ulong connectionHandle) {
        for (var i = Connections.Count - 1; i >= 0; i--) {
            if (Connections[i].HardwareID.Equals(hardwareID) &&
                Connections[i].ConnectionHandle != connectionHandle)
                Connections.RemoveAt(i);
        }
    }

    public bool HasDuplicateID(string hardwareID,ulong connectionHandle) {
        return Connections.Any(x => x.HardwareID == hardwareID && x.ConnectionHandle != connectionHandle);
    }

    public void CloseConnectionsWithCondition(Func<WiThrottleConnection, bool> conditionToClose, string logMessage) {
        var connectionsToClose = Connections.Where(conditionToClose).ToList();
        foreach (var connection in connectionsToClose) {
            connection.Close();
        }
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    public void Remove(WiThrottleConnection entry) {
        Connections.Remove(entry);
    }

    /// <summary>
    ///     Find an entry in the list by the UniqueID of the Throttle
    /// </summary>
    /// <param name="hardwareID">The unique hardware ID of the throttle</param>
    /// <returns>A entry of a connected throttle</returns>
    public WiThrottleConnection? Find(string hardwareID) => Connections.FirstOrDefault(x => x!.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));

    //public WiThrottleConnection? Find(ulong connectionID) => _connections.FirstOrDefault(x => x.ConnectionHandle == connectionID);
}