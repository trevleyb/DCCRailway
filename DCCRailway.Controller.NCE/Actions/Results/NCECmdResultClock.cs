using System;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DateTime = System.DateTime;

namespace DCCRailway.Controller.NCE.Actions.Results;

public class NCECmdResultClock : CmdResult, ICmdResultFastClock {
    public NCECmdResultClock(byte[]? dataSet) : base(dataSet) {
        if (Data.Length != 2) {
            Success = false;
        } else {
            Hour = Data[0];
            Min  = Data[1];
        }
    }

    public int Hour { get; }
    public int Min  { get; }

    public DateTime CurrentTime => new(DateTime.Now.Year,
                                       DateTime.Now.Month,
                                       DateTime.Now.Day,
                                       Hour, Min, 0);

    public string FastClock => $"{Hour:D2}:{Min:D2}";
}