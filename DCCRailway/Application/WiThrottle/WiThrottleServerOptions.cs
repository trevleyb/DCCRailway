using System.Net;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleServerOptions : BackgroundWorkerOptions {

    private const ushort DefaultPort = 12090;
    private const string DefaultTerminator  = "\x0A";
    private const string DefaultServiceName = "_withrottle._tcp";
    public readonly IRailwayConfig? Config;

    public ushort Port { get; set; } = DefaultPort;
    public string Terminator { get; set; } = DefaultTerminator;
    public string ServiceName { get; set; } = DefaultServiceName;
    public IPAddress Address { get; set; } = Network.GetLocalIPAddress();
    public Dictionary<string, string> Properties => new() {
        { "node", $"dccrailway-{Guid.NewGuid()}" },
        { "version", "1.0.0"}

        // This is what JMRI seems to broadcast
        //{ "node", "jmri-C4910CB13C68-3F39938d" },
        //{ "jmri", "4.21.4" },
        //{ "version", "4.2.1" }
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