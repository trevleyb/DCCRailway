using System;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.NCE.Commands;

public class NCECommandResultClock {
    private NCECommandResultClock(CommandResultDataSet dataSet) {
        if (dataSet == null || dataSet.Bytes != 2) throw new ApplicationException("Invalid data provided to create a Version");
        Hour = dataSet[0];
        Min  = dataSet[1];
    }
    public string FastClock => $"{Hour:D2}:{Min:D2}";
    public int Hour { get; }
    public int Min  { get; }
}