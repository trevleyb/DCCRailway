using System;
using System.Collections.Generic;
using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Simulator;

[Serializable]
public class SimulatedLocoList : Dictionary<int, SimulatedLocoEntry> {
    /// <summary>
    ///     Find an entry by a sinple index (int). Used to look for available slots for conisting
    ///     Returns NULL if there is no current entry in this slot position.
    /// </summary>
    public SimulatedLocoEntry? GetLoco(int Address) {
        if (ContainsKey(Address)) return this[Address];

        return null;
    }

    /// <summary>
    ///     Find an entry by a sinple index (int). Used to look for available slots for conisting
    ///     Returns NULL if there is no current entry in this slot position.
    /// </summary>
    public SimulatedLocoEntry? GetRandomLoco() {
        var max = Count;

        while (max > 0) {
            max -= 1;
            var findPos = new Random().Next(0, Count);

            if (this[findPos] != null && (this[findPos].Type == DCCAddressType.Short || this[findPos].Type == DCCAddressType.Long)) return this[findPos];
        }

        return null;
    }

    /// <summary>
    ///     Find an entry by address, and if it cannot be found, then add it and return the new entry.
    ///     This function will ALLWAYS return a valid entry
    /// </summary>
    public SimulatedLocoEntry GetLoco(DCCAddress Address) {
        if (!ContainsKey(Address.Address)) Add(Address.Address, new SimulatedLocoEntry(Address));

        return this[Address.Address];
    }
}