using System.Net;
using DCCRailway.Layout.Configuration;
using Makaretu.Dns;

namespace DCCRailway.WiThrottle.ServiceHelper;

public class ServerBroadcast() {
    private ServiceDiscovery? _sd;

    public void Start(WiThrottlePrefs options) {
        var hosts = Dns.GetHostEntry(Dns.GetHostName());

        try {
            var sp = new ServiceProfile(options.Name, options.ServiceName, (ushort)options.HostPort, hosts.AddressList);
            foreach (var prop in options.Properties) sp.AddProperty(prop.Key, prop.Value);
            _sd = new ServiceDiscovery();
            _sd.Advertise(sp);
        } catch (Exception ex) {
            throw new ApplicationException("WiThrottle Service Broadcast: Could not start Broadcast", ex);
        }
    }

    public void Stop() {
        if (_sd is not null) _sd.Unadvertise();
    }
}