using System.Net;
using DCCRailway.Common.Helpers;
using Makaretu.Dns;

namespace DCCRailway.Railway.Throttles.WiThrottle.Helpers;

public static class ServerBroadcast {
    public static void Start(WiThrottlePreferences options) {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        if (host.AddressList.Length > 0) {
            var addressList = new List<IPAddress>() { options.HostAddress };
            try {
                var sd = new ServiceDiscovery();

                //sd.ServiceDiscovered         += Sd_ServiceDiscovered;
                //sd.ServiceInstanceShutdown   += Sd_ServiceInstanceShutdown;
                //sd.ServiceInstanceDiscovered += Sd_ServiceInstanceDiscovered;
                sd.AnswersContainsAdditionalRecords = true;
                var sp = new ServiceProfile(options.Name, options.ServiceName, (ushort)options.HostPort, host.AddressList);
                foreach (var prop in options.Properties) {
                    sp.AddProperty(prop.Key, prop.Value);
                }
                sd.AnswersContainsAdditionalRecords = true;
                sd.Advertise(sp);
            } catch (Exception ex) {
                throw new ApplicationException("Could not start Broadcast", ex);
            }
        } else {
            throw new ApplicationException("Could not Broadcast since cannot determine local IP Addresses.");
        }
    }

    private static void Sd_ServiceInstanceShutdown(object? sender, ServiceInstanceShutdownEventArgs e) => Logger.Log.Debug($"SD: Shutdown=>{e.Message}");

    private static void Sd_ServiceInstanceDiscovered(object? sender, ServiceInstanceDiscoveryEventArgs e) => Logger.Log.Debug($"SD: Instance Discovered=>{e.Message}");

    private static void Sd_ServiceDiscovered(object? sender, DomainName e) => Logger.Log.Debug($"SD: Service Discovered=>{e.Labels}");
}