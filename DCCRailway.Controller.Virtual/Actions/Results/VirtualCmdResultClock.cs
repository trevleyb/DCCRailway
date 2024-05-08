using System;
using DCCRailway.Controller.Actions.Results.Abstract;

namespace DCCRailway.Controller.Virtual.Actions.Results;

public class VirtualCmdResultClock : CmdResult {

    public VirtualCmdResultClock(int hour, int minute, int ratio) : base((true)) {
        Hour = hour;
        Min = minute;
        Ratio = ratio;
        SetTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Min, 0);
    }

    public string FastClock => $"{Hour:D2}:{Min:D2}";
    public int    Hour      { get; }
    public int    Min       { get; }
    public int    Ratio     { get; }
    public DateTime SetTime { get; }
}