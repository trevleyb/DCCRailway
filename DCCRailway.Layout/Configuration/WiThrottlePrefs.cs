using System.Net;
using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Layout.Configuration;

[Serializable]
public class WiThrottlePrefs {
    private const ushort _defaultPort        = 12090;
    private const string _defaultServiceName = "_withrottle._tcp";

    public string  Name             { get; set; } = "DCCRailway WiThrottle Service";
    public int?    Port             { get; set; }
    public bool    UseFastClock     { get; set; } = false;
    public int     HeartbeatSeconds { get; set; } = 15;
    public bool    RunOnStartup     { get; set; } = false;

    [JsonIgnore]
    public int HostPort => Port ??  _defaultPort;

    [JsonIgnore]
    public int HeartbeatCheckTime => HeartbeatSeconds / 5 * 1000;

    [JsonIgnore]
    public string ServiceName => _defaultServiceName;

    [JsonIgnore]
    public Dictionary<string, string> Properties => new() {
        { "node", $"dccrailway-{Guid.NewGuid()}" },
        { "version", "1.0.0" }
    };
}