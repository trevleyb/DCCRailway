namespace DCCRailway.Common.Configuration;

public class FastClock {
    private DateTime       _started;
    private FastClockState _state = FastClockState.Stop;
    public  int            Ratio { get; set; } = 4;

    public FastClockState State {
        get => _state;
        set {
            switch (value) {
            case FastClockState.Start or FastClockState.Reset:
                _started = DateTime.Now;
                _state   = FastClockState.Running;
                break;
            case FastClockState.Stop:
                _started = DateTime.Now;
                _state   = FastClockState.Stop;
                break;
            }
        }
    }

    public DateTime ClockTime {
        get {
            var currentTime    = DateTime.Now;
            var timeDifference = currentTime.Subtract(_started);
            var elapsedSeconds = (int)timeDifference.TotalSeconds;
            var fastSeconds    = elapsedSeconds * Ratio;
            return _started.AddSeconds(fastSeconds);
        }
    }
}

public enum FastClockState {
    Start,
    Stop,
    Running,
    Reset
}