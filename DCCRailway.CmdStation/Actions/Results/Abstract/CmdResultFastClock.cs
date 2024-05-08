namespace DCCRailway.CmdStation.Actions.Results.Abstract;

public class CmdResultFastClock(DateTime clock) : CmdResult, ICmdResult {
    public DateTime CurrentTime = clock;
}