using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Server;

/// <summary>
///     This class tracks what locos are owned or assigned to a particular Throttle
/// </summary>
public class AssignedLocos {
    public readonly List<AssignedLoco> AssignedAddresses = [];

    public int Count => AssignedAddresses.Count;

    public AssignedLoco? Get(DCCAddress address) {
        var entry = AssignedAddresses.FirstOrDefault(assigned => assigned.Address.Address.Equals(address.Address));
        return entry;
    }

    public bool IsAssigned(DCCAddress address) {
        return AssignedAddresses.Any(assigned => assigned.Address.Address.Equals(address.Address));
    }

    public bool Acquire(char group, DCCAddress address, Connection connection) {
        if (!IsAssigned(address)) {
            AssignedAddresses.Add(new AssignedLoco { Connection = connection, Group = group, Address = address });
            var layoutCmd = new LayoutCmdHelper(connection.CommandStation, address);
            layoutCmd.Acquire();
            return true;
        }

        return false;
    }

    public List<DCCAddress> ReleaseAllInGroup(char dataGroup, Connection connection) {
        var addresses  = new List<DCCAddress>();
        var foundLocos = new List<AssignedLoco>();

        foreach (var loco in AssignedAddresses)
            if (loco.Group == dataGroup && loco.Connection == connection) {
                foundLocos.Add(loco);
                addresses.Add(loco.Address);
            }

        foreach (var loco in foundLocos) Release(loco.Address);
        return addresses;
    }

    public List<DCCAddress> GetAllAssignedForConnection(Connection connection, char group) {
        var addresses = new List<DCCAddress>();
        foreach (var loco in AssignedAddresses) {
            if (loco.Group == group && loco.Connection == connection) addresses.Add(loco.Address);
        }

        return addresses;
    }

    // Release a Loco, but return what Connection OWNED that Loco as we need to
    // advise that Throttle that they no longer have the loco.
    public Connection? Release(DCCAddress address) {
        var loco = AssignedAddresses.FirstOrDefault(x => x.Address.Address == address.Address);

        if (loco is not null) {
            var layoutCmd = new LayoutCmdHelper(loco.Connection.CommandStation, loco.Address);
            layoutCmd.Release();
            layoutCmd.Dispatch();
            AssignedAddresses.Remove(loco);
            return loco.Connection;
        }

        return null;
    }
}