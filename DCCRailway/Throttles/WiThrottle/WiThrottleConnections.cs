using System.Net.Sockets;
using DCCRailway.CmdStation;
using DCCRailway.Common.Types;
using DCCRailway.Configuration;

namespace DCCRailway.Throttles.WiThrottle;

/// <summary>
///     ConnectionInfo stores the details of each connected throttle to the controller
///     and will track a connection to a Command Station and what commands are or
///     have been sent on behalf of the throttle.
/// </summary>
public class WiThrottleConnections : List<WiThrottleConnection> {
    /// <summary>
    ///     Add a new entry into the list of connected Throttles
    /// </summary>
    public WiThrottleConnection Add(TcpClient client, WiThrottlePreferences preferences, IRailwayConfig railwayConfig, CmdStationManager cmdStationMgr) {
        var connection = Find((ulong)client.Client.Handle);
        if (connection is null) {
            connection = new WiThrottleConnection(client, preferences, this, railwayConfig, cmdStationMgr);
            Add(connection);
        }
        return connection;
    }

    public bool IsAddressInUse(DCCAddress address) {
        foreach (var connection in this) {
            if (connection.IsLocoAssigned(address)) return true;
        }
        return false;
    }

    public void Release(DCCAddress address) {
        foreach (var connection in this) {
            if (connection.IsLocoAssigned(address)) connection.Release(address);
        }
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    public void Disconnect(WiThrottleConnection entry) {
        // Is there anything we need to do to close this?
        Remove(entry);
    }

    /// <summary>
    ///     Find an entry in the list by the UniqueID of the Throttle
    /// </summary>
    /// <param name="hardwareID">The unique hardware ID of the throttle</param>
    /// <returns>A entry of a connected throttle</returns>
    public WiThrottleConnection? Find(string hardwareID) => this.FirstOrDefault(x => x!.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));

    /// <summary>
    ///     Find an entry by its connection Handle
    /// </summary>
    /// <param name="connectionID">the TCP Client handle for the connection</param>
    /// <returns>A entry of a connected throttle</returns>
    public WiThrottleConnection? Find(ulong connectionID) => this.FirstOrDefault(x => x.ConnectionHandle == connectionID);
}