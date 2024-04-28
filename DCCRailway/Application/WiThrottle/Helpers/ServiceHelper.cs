using System.Diagnostics;
using System.Net.NetworkInformation;

namespace DCCRailway.Application.WiThrottle.Helpers;

public static class ServiceHelper {

    public static bool IsServiceRunningOnPort(int port)
    {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpConnections = ipGlobalProperties.GetActiveTcpConnections();
        return tcpConnections.Any(x => x.LocalEndPoint.Port.Equals(port));
    }

    public static void TerminateService(int port)
    {
        // Get the process associated with the service
        var processes = Process.GetProcesses();
        foreach (var process in processes) {
            try {
                if (process.HasExited) continue;
                if (process.Id <= 4) continue; // Skip system processes

                var modules = process.Modules;
                foreach (ProcessModule module in modules) {
                    if (module.FileName.Contains($"\\{port}\\")) {
                        process.Kill();
                        return;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error terminating process: {ex.Message}");
            }
        }
    }

//    public static bool IsServiceRunning(string serviceName) {
//        var allProcesses = Process.GetProcesses();
//        var serviceProcesses = allProcesses.Where(x => x.ProcessName.Contains(serviceName, StringComparison.InvariantCultureIgnoreCase));
//        return serviceProcesses.Any();
//    }
}