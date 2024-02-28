using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Manufacturer.NCE.Commands.Results;

public class NCECommandResultClock : CommandResult {
    
    public NCECommandResultClock(CommandResultData? dataSet) : base(true, dataSet, null) {
        if (dataSet == null || dataSet.Length != 2) {
            this.IsSuccess = false;
        } else {
            Hour = dataSet[0];
            Min  = dataSet[1];
        }
    }
    public string FastClock => $"{Hour:D2}:{Min:D2}";
    public int Hour { get; }
    public int Min  { get; }
}