using System.Net.Sockets;
using DCCRailway.Controller.Controllers;
using DCCRailway.Layout;
using DCCRailway.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.WiThrottle;

/// <summary>
///     ConnectionInfo stores the details of each connected throttle to the commandStation
///     and will track a connection to a Command Station and what commands are or
///     have been sent on behalf of the throttle.
/// </summary>
public class Connections(ILogger logger) {
    public readonly List<Connection> ActiveConnections = [];

    public readonly AssignedLocos Assignments = new();
    public          int           Count => ActiveConnections.Count;

    /// <summary>
    ///     Add a new entry into the list of connected Throttles
    /// </summary>
    public Connection Add(TcpClient client, IRailwaySettings railwaySettings, ICommandStation commandStation) {
        var connection = new Connection(client, railwaySettings, this, commandStation);
        ActiveConnections.Add(connection);
        return connection;
    }

    /// <summary>
    /// Used particularly for changes in Turnout, Power and Routes, send the status update
    /// message to all connected devices.
    /// </summary>
    /// <param name="message"></param>
    public void QueueMsgToAll(IThrottleMsg message) {
        foreach (var connection in ActiveConnections) connection.QueueMsg(message);
    }

    public void QueueMsgToAll(IThrottleMsg[] messages) {
        foreach (var connection in ActiveConnections)
        foreach (var msg in messages)
            connection.QueueMsg(msg);
    }

    public Connection? GetByHardwareID(string hardwareID, ulong connectionHandle) {
        return ActiveConnections.FirstOrDefault(x => x.HardwareID.Equals(hardwareID) && x.ConnectionHandle != connectionHandle);
    }

    public void RemoveDuplicateID(string hardwareID, ulong connectionHandle) {
        for (var i = ActiveConnections.Count - 1; i >= 0; i--)
            if (ActiveConnections[i].HardwareID.Equals(hardwareID) && ActiveConnections[i].ConnectionHandle != connectionHandle)
                ActiveConnections.RemoveAt(i);
    }

    public bool HasDuplicateID(string hardwareID, ulong connectionHandle) {
        return ActiveConnections.Any(x => x.HardwareID == hardwareID && x.ConnectionHandle != connectionHandle);
    }

    public void CloseConnectionsWithCondition(Func<Connection, bool> conditionToClose, string logMessage) {
        var connectionsToClose = ActiveConnections.Where(conditionToClose).ToList();

        foreach (var connection in connectionsToClose) {
            logger.Information("WiThrottle Connection: Closing Client '{0}' due to no heartbeat.", connection.ConnectionHandle);
            connection.Close();
        }
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    public void Remove(Connection entry) {
        ActiveConnections.Remove(entry);
    }

    /// <summary>
    ///     Find an entry in the list by the UniqueID of the Throttle
    /// </summary>
    /// <param name="hardwareID">The unique hardware ID of the throttle</param>
    /// <returns>A entry of a connected throttle</returns>
    public Connection? Find(string hardwareID) {
        return ActiveConnections.FirstOrDefault(x => x!.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));
    }

    //public WiThrottleConnection? Find(ulong connectionID) => _connections.FirstOrDefault(x => x.ConnectionHandle == connectionID);
}