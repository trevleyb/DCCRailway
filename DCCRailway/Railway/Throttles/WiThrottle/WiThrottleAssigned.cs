using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Throttles.WiThrottle;

/// <summary>
///     This class tracks what locos are owned or assigned to a particular Throttle
/// </summary>
public class WiThrottleAssignedLocos {
    private readonly Dictionary<char, List<DCCAddress>> _assignedAddresses = [];

    public int    Count  => _assignedAddresses.Sum(x => x.Value.Count);
    public char[] Groups => _assignedAddresses.Keys.ToArray();

    public IEnumerable<DCCAddress> AssignedLocos => _assignedAddresses.Values.SelectMany(addresses => addresses);

    public bool IsAssigned(DCCAddress address) {
        return _assignedAddresses.Values.Any(list => list.Any(x => x.Address == address.Address));
    }

    public bool Assign(char group, DCCAddress address) {
        if (!_assignedAddresses.ContainsKey(group)) _assignedAddresses[group] = new List<DCCAddress>();
        if (_assignedAddresses[group].Any(x => x.Address == address.Address)) return true;
        _assignedAddresses[group].Add(address);
        return false;
    }

    public bool Release(DCCAddress address) {
        foreach (var group in _assignedAddresses.Keys) {
            var entry = _assignedAddresses[group].FirstOrDefault(x => x.Address == address.Address) ?? null;
            if (entry is not null) _assignedAddresses[group].Remove(entry);
        }
        return false;
    }

    public void Clear() {
        _assignedAddresses.Clear();
    }
}