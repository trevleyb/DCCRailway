namespace DCCRailway.Controller.Actions.Results.Abstract;

public class CmdResultFastClock() : CmdResult, ICmdResultFastClock {
    public DateTime CurrentTime { get; set; }
}