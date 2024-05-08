using DCCRailway.CmdStation.Actions.Results.Abstract;

namespace DCCRailway.CmdStation.NCE.Actions.Results;

public class NCECmdResultClock : CmdResult {
    public NCECmdResultClock(byte[]? dataSet) : base(dataSet) {

        if (Data.Length != 2) {
            Success = false;
        }
        else {
            Hour = Data[0];
            Min  = Data[1];
        }
    }

    public int    Hour      { get; }
    public int    Min       { get; }
    public string FastClock => $"{Hour:D2}:{Min:D2}";
}