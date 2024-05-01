using System.Net.Sockets;

namespace DCCRailway.Application.WiThrottle;

/// <summary>
///     ConnectionInfo stores the details of each connected throttle to the controller
///     and will track a connection to a Command Station and what commands are or
///     have been sent on behalf of the throttle.
/// </summary>
public class WiThrottleConnections : List<WiThrottleConnection> {

    /// <summary>
    ///     Add a new entry into the list of connected Throttles
    /// </summary>
    /// <param name="connectionID">The connectinID for this throttle</param>
    /// <param name="throttleName">The name of the Throttle</param>
    /// <param name="hardwareID">The unique ID for the throttle</param>
    /// <returns></returns>
    public WiThrottleConnection Add(ulong connectionID, string throttleName = "", string hardwareID = "") {
        var connection = Find(connectionID);
        if (connection is null) {
            connection = new(connectionID, this, throttleName, hardwareID) {
                HardwareID = hardwareID,
                ThrottleName = throttleName
            };
            Add(connection);
        }
        return connection;
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    /// <param name="connectionID"></param>
    public void Disconnect(ulong connectionID) {
        foreach (var entry in FindAll(x => x.ConnectionID == connectionID)) {
            // Is there anything we need to do to close this?
        }
        RemoveAll(x => x.ConnectionID == connectionID);
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    /// <param name="connectionID"></param>
    public void Disconnect(WiThrottleConnection entry) {
        // Is there anything we need to do to close this?
        Remove(entry);
    }

    /// <summary>
    ///     Remove an entry from the entries list
    /// </summary>
    /// <param name="hardwareID"></param>
    public void Disconnect(string hardwareID) {
        foreach (var entry in FindAll(x => x.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase))) {
            // Is there anything we need to do to close this?
        }
        RemoveAll(x => x.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    ///     Remove an entry from the entries list
    /// </summary>
    public void Delete(WiThrottleConnection entry) {
        Disconnect(entry);
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
    public WiThrottleConnection? Find(ulong connectionID) => this.FirstOrDefault(x => x.ConnectionID == connectionID);
}