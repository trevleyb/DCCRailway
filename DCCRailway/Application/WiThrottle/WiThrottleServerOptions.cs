using System.Net;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleServerOptions (IRailwayConfig config, IController? controller, string name = "DCCRailway WiThrottle Service") {

    private const ushort DefaultPort = 12090;
    private const string DefaultServiceName = "_withrottle._tcp";
    public ushort Port { get; set; } = DefaultPort;
    public string ServiceName { get; set; } = DefaultServiceName;
    public IPAddress Address { get; set; } = Network.GetLocalIPAddress();
    public int HeartbeatSeconds { get; set; } = 15;
    public string Name { get; init; } = name;
    public readonly IRailwayConfig? Config = config;
    public readonly IController? Controller = controller;

    public Dictionary<string, string> Properties => new() {
        { "node", $"dccrailway-{Guid.NewGuid()}" },
        { "version", "1.0.0"}
    };
}