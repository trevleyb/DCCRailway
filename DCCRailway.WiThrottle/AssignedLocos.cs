using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle;

/// <summary>
///     This class tracks what locos are owned or assigned to a particular Throttle
/// </summary>
public class AssignedLocos
{
    public readonly List<AssignedLoco> AssignedAddresses = [];

    public int Count => AssignedAddresses.Count;

    public bool IsAssigned(DCCAddress address) => AssignedAddresses.Any(assigned => assigned.Address.Equals(address));

    public bool Acquire(char group, DCCAddress address, Connection connection)
    {
        if (!IsAssigned(address))
        {
            AssignedAddresses.Add(new AssignedLoco { Connection = connection, Group = group, Address = address });
            return true;
        }

        return false;
    }

    public List<DCCAddress> ReleaseAllInGroup(char dataGroup, Connection connection)
    {
        var addresses  = new List<DCCAddress>();
        var foundLocos = new List<AssignedLoco>();
        foreach (var loco in AssignedAddresses)
        {
            if (loco.Group == dataGroup && loco.Connection == connection)
            {
                foundLocos.Add(loco);
                addresses.Add(loco.Address);
            }
        }

        foreach (var loco in foundLocos) AssignedAddresses.Remove(loco);
        return addresses;
    }

    // Release a Loco, but return what Connection OWNED that Loco as we need to
    // advise that Throttle that they no longer have the loco.
    public Connection? Release(DCCAddress address)
    {
        var loco = AssignedAddresses.FirstOrDefault(x => x.Address.Address == address.Address);
        if (loco is not null)
        {
            AssignedAddresses.Remove(loco);
            return loco.Connection;
        }

        return null;
    }
}