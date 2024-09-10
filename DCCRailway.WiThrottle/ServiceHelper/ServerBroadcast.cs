using System.Net;
using DCCRailway.Common.Configuration;
using Makaretu.Dns;
using Serilog;

namespace DCCRailway.WiThrottle.ServiceHelper;

public class ServerBroadcast(ILogger? logger) {
    private ServiceDiscovery? _sd;

    public void Start(string url)              => Start("DCCRailway", "_dccrailway._tcp", GetPort(url));
    public void Start(ushort port)             => Start("DCCRailway", "_dccrailway._tcp", port);
    public void Start(WiThrottlePrefs options) => Start(options.Name, options.ServiceName, (ushort)options.HostPort, options.Properties);

    public void Start(string name, string serviceName, ushort port, Dictionary<string, string>? properties = null) {
        var hosts = Dns.GetHostEntry(Dns.GetHostName());
        try {
            logger?.Information("Starting Broadcast {0} as {1} on {2}", name, serviceName, port);
            var sp = new ServiceProfile(name, serviceName, port, hosts.AddressList);
            if (properties is not null) {
                foreach (var prop in properties) sp.AddProperty(prop.Key, prop.Value);
            }

            _sd = new ServiceDiscovery();
            _sd.Advertise(sp);
        } catch (Exception ex) {
            throw new ApplicationException($"{name} Service Broadcast: Could not start Broadcast", ex);
        }
    }

    public void Stop() {
        logger?.Information("Stopping WiThrottle Service Broadcast");
        if (_sd is not null) _sd.Unadvertise();
    }

    public ushort GetPort(string url) {
        if (Uri.TryCreate(url, UriKind.Absolute, out var uri)) {
            return (ushort)uri.Port;
        }

        return 0;
    }
}