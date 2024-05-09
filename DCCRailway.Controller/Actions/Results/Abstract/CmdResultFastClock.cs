namespace DCCRailway.Controller.Actions.Results.Abstract;

public class CmdResultFastClock(DateTime clock) : CmdResult, ICmdResultFastClock {
    public DateTime CurrentTime { get; set; }
}