using System.Net;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleServerOptions : BackgroundWorkerOptions {

    private const ushort DefaultPort = 12090;
    private const string DefaultServiceName = "_withrottle._tcp";
    public readonly IRailwayConfig? Config;

    public ushort Port { get; set; } = DefaultPort;
    public string ServiceName { get; set; } = DefaultServiceName;
    public IPAddress Address { get; set; } = Network.GetLocalIPAddress();
    public Dictionary<string, string> Properties => new() {
        { "node", $"dccrailway-{Guid.NewGuid()}" },
        { "version", "1.0.0"}
    };

    /// <summary>
    /// The contructor needs to take a reference to the Railway Config object so that it can get information
    /// on Turnouts, Routes, and Locomotives
    /// </summary>
    /// <param name="config">A reference to the Railway Config file</param>
    /// <param name="name">The name that WiThrottle will report</param>
    public WiThrottleServerOptions(IRailwayConfig config, string name = "DCCRailway WiThrottle Service") {
        Name = name;
        FrequencyInSeconds = 0;
        Config = config;
    }
}