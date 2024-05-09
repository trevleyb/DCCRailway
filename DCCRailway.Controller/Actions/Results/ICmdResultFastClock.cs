namespace DCCRailway.Controller.Actions.Results;

public interface ICmdResultFastClock : ICmdResult {
    DateTime CurrentTime { get; }
}