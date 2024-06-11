using System.Net;
using System.Net.Sockets;
using Makaretu.Dns;

namespace DCCRailway.WiThrottle;

public static class ServiceFinder {
    /// <summary>
    /// Uses MDNS to try and find services with the given name (such as WiThrottle) and then finds the
    /// appropriate IPAddress for that service. It will return a collection of found services so that
    /// one can be selected to connect to. 
    /// </summary>
    /// <param name="serviceName">The name, or part name, to search for</param>
    /// <param name="timeout">How long should finding this take</param>
    /// <returns>A collection of Names and IPAddresses that match the service name</returns>
    public static List<FoundService> FindServices(string serviceName, int timeout = 2000) {
        List<FoundService> foundServices = [];
        var                sd            = new ServiceDiscovery();

        sd.ServiceDiscovered += (s, domainName) => {
            if (CheckDomainNameComponents(domainName, serviceName)) {
                sd.QueryServiceInstances(domainName.ToString().Replace(".local", ""));
            }
        };

        sd.ServiceInstanceDiscovered += (s, e) => {
            if (CheckDomainNameComponents(e.ServiceInstanceName, serviceName)) {
                var query = new Message();
                query.Questions.Add(new Question { Name = e.ServiceInstanceName, Type = DnsType.SRV });
                sd.Mdns.SendQuery(query);
            }
        };

        sd.Mdns.AnswerReceived += (s, e) => {
            foreach (var answer in e.Message.Answers) {
                if (answer is SRVRecord srv) {
                    if (CheckDomainNameComponents(srv.Name, serviceName)) {
                        var foundIps = Dns.GetHostAddresses(srv.Target.ToString());
                        foundServices.AddRange(from ip in foundIps where ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString() != "127.0.0.1" select new FoundService(srv.Name.ToString(), new ClientInfo(ip.ToString(), srv.Port)));
                    }
                }
            }
        };

        sd.Mdns.Start();
        sd.QueryAllServices();
        Thread.Sleep(timeout);
        sd.Mdns.Stop();
        return foundServices;
    }

    private static bool CheckDomainNameComponents(DomainName domainName, string serviceName) {
        return domainName.ToString().Contains(serviceName, StringComparison.OrdinalIgnoreCase) || domainName.Labels.Any(part => part.Contains(serviceName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Record containing the found information for a service
    /// </summary>
    /// <param name="Name">The found name of the service</param>
    /// <param name="Address">The Internetwork address (IPV4) of the service</param>
    /// <param name="Port">The port that the service is running on</param>
    public record FoundService(string Name, ClientInfo ClientInfo);
}