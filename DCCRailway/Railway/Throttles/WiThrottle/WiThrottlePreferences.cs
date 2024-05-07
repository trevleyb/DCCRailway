using System.Net;
using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle;

public class WiThrottlePreferences () : JsonSerializerHelper<WiThrottlePreferences> {

    private const ushort DefaultPort = 12090;
    private const string DefaultServiceName = "_withrottle._tcp";
    private const string DefaultFileName = "DCCRailway.Withrottle.Json";

    public IPAddress Address { get; set; } = Network.GetLocalIPAddress();
    public ushort Port { get; set; } = DefaultPort;
    public string ServiceName { get; set; } = DefaultServiceName;
    public string Name { get; set; } = "DCCRailway WiThrottle Service";
    public bool UseFastClock { get; set; }

    public int HeartbeatSeconds { get; set; } = 15;
    public int HeartbeatCheckTime => (int)((HeartbeatSeconds / 5) * 1000);

    /// <summary>
    /// This collection tracks a list of connections from different WiThrottles
    /// </summary>
    [JsonIgnore]
    public WiThrottleConnections Connections { get; } = [];

    public Dictionary<string, string> Properties => new() {
        { "node", $"dccrailway-{Guid.NewGuid()}" },
        { "version", "1.0.0"}
    };

    public static WiThrottlePreferences? Load() => LoadFile(DefaultFileName);
    public void                          Save() => SaveFile(this, DefaultFileName);
    public static WiThrottlePreferences? Load(string name) => LoadFile(name);
    public void                          Save(string name) => SaveFile(this, name);

}