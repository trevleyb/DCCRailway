using System.Net.Sockets;

namespace DCCRailway.Application.WiThrottle;

/// <summary>
///     ConnectionInfo stores the details of each connected throttle to the controller
///     and will track a connection to a Command Station and what commands are or
///     have been sent on behalf of the throttle.
/// </summary>
public class WiThrottleConnectionList {
    public readonly List<WiThrottleConnectionEntry> Entries = new();

    /// <summary>
    ///     Add a new entry into the list of connected Throttles
    /// </summary>
    /// <param name="connectionID">The connectinID for this throttle</param>
    /// <param name="throttleName">The name of the Throttle</param>
    /// <param name="hardwareID">The unique ID for the throttle</param>
    /// <returns></returns>
    public WiThrottleConnectionEntry? Add(ulong connectionID, string throttleName = "", string hardwareID = "") {
        if (Find(connectionID) == null) {
            WiThrottleConnectionEntry entry = new(connectionID, this, throttleName, hardwareID);
            entry.HardwareID   = hardwareID;
            entry.ThrottleName = throttleName;
            Entries.Add(entry);
            return entry;
        }
        return Find(connectionID);
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    /// <param name="connectionID"></param>
    public void Disconnect(ulong connectionID) {
        foreach (var entry in Entries.FindAll(x => x.ConnectionID == connectionID)) {
            // Is there anything we need to do to close this?
        }
        Entries.RemoveAll(x => x.ConnectionID == connectionID);
    }

    /// <summary>
    ///     Remove an Entry from the entries list
    /// </summary>
    /// <param name="connectionID"></param>
    public void Disconnect(WiThrottleConnectionEntry entry) {
        // Is there anything we need to do to close this?
        Entries.Remove(entry);
    }

    /// <summary>
    ///     Remove an entry from the entries list
    /// </summary>
    /// <param name="hardwareID"></param>
    public void Disconnect(string hardwareID) {
        foreach (var entry in Entries.FindAll(x => x.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase))) {
            // Is there anything we need to do to close this?
        }
        Entries.RemoveAll(x => x.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    ///     Remove an entry from the entries list
    /// </summary>
    /// <param name="hardwareID"></param>
    public void Delete(WiThrottleConnectionEntry entry) {
        Disconnect(entry);
    }

    /// <summary>
    ///     Find an entry in the list by the UniqueID of the Throttle
    /// </summary>
    /// <param name="hardwareID">The unique hardware ID of the throttle</param>
    /// <returns>A entry of a connected throttle</returns>
    public WiThrottleConnectionEntry? Find(string hardwareID) => Entries.FirstOrDefault(x => x!.HardwareID.Equals(hardwareID, StringComparison.InvariantCultureIgnoreCase));

    /// <summary>
    ///     Find an entry by its connection Handle
    /// </summary>
    /// <param name="connectionID">the TCP Client handle for the connection</param>
    /// <returns>A entry of a connected throttle</returns>
    public WiThrottleConnectionEntry? Find(ulong connectionID) => Entries.FirstOrDefault(x => x.ConnectionID == connectionID);
}