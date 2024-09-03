using System.Net.NetworkInformation;

namespace DCCRailway.WiThrottle.ServiceHelper;

public static class ServiceHelper {
    public static bool IsServiceRunningOnPort(int port) {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpConnections     = ipGlobalProperties.GetActiveTcpConnections();
        var listenersOnPort    = tcpConnections.Where(x => x.LocalEndPoint.Port.Equals(port));
        return listenersOnPort.Any();
    }
}