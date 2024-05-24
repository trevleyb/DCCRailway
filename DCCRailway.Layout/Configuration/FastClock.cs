namespace DCCRailway.Layout.Configuration;

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
            var secondsElapsed = DateTime.Now.Subtract(_started);
            var fastSeconds    = secondsElapsed.Seconds * Ratio;
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