namespace DCCRailway.CmdStation.Commands.Results;

public class CommandResultFastClock : CommandResult, ICommandResult {

    public DateTime CurrentTime;
    public CommandResultFastClock(DateTime clock) : base(true) {
        CurrentTime = clock;
    }
}