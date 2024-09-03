using System.Text.Json.Serialization;

namespace DCCRailway.Layout.Configuration;

[Serializable]
public class WiThrottlePrefs {
    private const ushort _defaultPort        = 12090;
    private const string _defaultServiceName = "_withrottle._tcp";

    public string Name                 { get; set; } = "DCCRailway.WiThrottle Service";
    public int?   Port                 { get; set; }
    public bool   UseFastClock         { get; set; } = false;
    public int    FastClockSeconds     { get; set; } = 15;
    public int    HeartbeatSeconds     { get; set; } = 15;
    public bool   RunOnStartup         { get; set; } = false;
    public bool   ZeroSpeedOnDirection { get; set; } = false;

    [JsonIgnore] public int    HostPort           => Port ?? _defaultPort;
    [JsonIgnore] public int    HeartbeatCheckTime => int.Max(HeartbeatSeconds, 5) + 5;
    [JsonIgnore] public int    FastClockCheckTime => int.Max(FastClockSeconds, 5);
    [JsonIgnore] public string ServiceName        => _defaultServiceName;

    [JsonIgnore]
    public Dictionary<string, string> Properties =>
        new() {
            { "node", $"dccrailway-{Guid.NewGuid()}" },
            { "version", "1.0.0" }
        };
}