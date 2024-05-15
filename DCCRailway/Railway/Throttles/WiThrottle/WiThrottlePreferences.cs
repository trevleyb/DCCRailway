using System.Net;
using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle;

[Serializable]
public class WiThrottlePreferences : JsonSerializerHelper<WiThrottlePreferences> {
    private const ushort _defaultPort        = 12090;
    private const string _defaultServiceName = "_withrottle._tcp";

    public string  Name             { get; set; } = "DCCRailway WiThrottle Service";
    public string? Address          { get; set; }
    public string? Port             { get; set; }
    public bool    UseFastClock     { get; set; } = false;
    public int     HeartbeatSeconds { get; set; } = 15;

    [JsonIgnore]
    public IPAddress HostAddress => string.IsNullOrEmpty(Address) ? Network.GetLocalIPAddress() : IPAddress.Parse(Address);

    [JsonIgnore]
    public int HostPort => string.IsNullOrEmpty(Port) ? _defaultPort : int.Parse(Port);

    [JsonIgnore]
    public int HeartbeatCheckTime => HeartbeatSeconds / 5 * 1000;

    [JsonIgnore]
    public string ServiceName => _defaultServiceName;

    /// <summary>
    ///     This collection tracks a list of connections from different WiThrottles
    /// </summary>
    [JsonIgnore]
    public WiThrottleConnections Connections { get; } = [];

    [JsonIgnore]
    public Dictionary<string, string> Properties => new() {
        { "node", $"dccrailway-{Guid.NewGuid()}" },
        { "version", "1.0.0" }
    };
}