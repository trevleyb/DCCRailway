using System.Net;
using DCCRailway.Common.Helpers;
using Makaretu.Dns;
using Serilog;

namespace DCCRailway.Railway.Throttles.WiThrottle.Helpers;

public class ServerBroadcast(ILogger logger) {
    public void Start(WiThrottlePreferences options) {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        if (host.AddressList.Length > 0) {
            var addressList = new List<IPAddress> { options.HostAddress };
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

    private void Sd_ServiceInstanceShutdown(object? sender, ServiceInstanceShutdownEventArgs e) => logger.ForContext<ServerBroadcast>().Debug($"SD: Shutdown=>{e.Message}");

    private void Sd_ServiceInstanceDiscovered(object? sender, ServiceInstanceDiscoveryEventArgs e) => logger.ForContext<ServerBroadcast>().Debug($"SD: Instance Discovered=>{e.Message}");

    private void Sd_ServiceDiscovered(object? sender, DomainName e) => logger.ForContext<ServerBroadcast>().Debug($"SD: Service Discovered=>{e.Labels}");
}