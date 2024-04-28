using System.Net;
using DCCRailway.Common.Helpers;
using Makaretu.Dns;

namespace DCCRailway.Application.WiThrottle.Helpers;

public class ServerBroadcast {
    public static void Start(WiThrottleServerOptions options) {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        if (host.AddressList.Length > 0) {
            var addressList = new List<IPAddress>() { options.Address };
            try {
                var sd = new ServiceDiscovery();
                sd.AnswersContainsAdditionalRecords = true;

                //sd.ServiceInstanceShutdown += Sd_ServiceInstanceShutdown;
                var sp = new ServiceProfile(options.Name, options.ServiceName, options.Port, host.AddressList);
                foreach (var prop in options.Properties) sp.AddProperty(prop.Key,prop.Value);

                //sp.AddProperty ("Version", "0.0.0.1");
                //sp.AddProperty ("Server", "DCCRailway");
                sd.AnswersContainsAdditionalRecords = true;
                sd.Advertise(sp);
            }
            catch (Exception ex) {
                throw new ApplicationException("Could not start Broadcast", ex);
            }
        }
        else {
            throw new ApplicationException("Could not Broadcast since cannot determine local IP Addresses.");
        }
    }

    private void Sd_ServiceInstanceShutdown(object? sender, ServiceInstanceShutdownEventArgs e) => Logger.Log.Debug($"SD: Shutdown=>{e.Message}");

    private void Sd_ServiceInstanceDiscovered(object? sender, ServiceInstanceDiscoveryEventArgs e) => Logger.Log.Debug($"SD: Instance Discovered=>{e.Message}");

    private void Sd_ServiceDiscovered(object? sender, DomainName e) => Logger.Log.Debug($"SD: Service Discovered=>{e.Labels}");
}