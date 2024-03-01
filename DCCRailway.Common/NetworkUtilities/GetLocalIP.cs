using System.Net;
using System.Net.Sockets;

namespace DCCRailway.DCCServer.NetworkUtilities;

public static class Network {
    /// <summary>
    ///     Get the localhost IP Address for this machine
    /// </summary>
    /// <returns>IPAddress as a String</returns>
    /// <exception cref="Exception">Returns an error if there is no networking </exception>
    public static IPAddress GetLocalIPAddress() {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork && !ip.ToString().Equals("127.0.0.1")) return ip;
        }

        throw new Exception("No network adapters with an IPv4 address in the controller!");
    }
}