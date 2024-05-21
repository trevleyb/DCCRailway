using System.Net;
using System.Text;

namespace DCCRailway.WiThrottle.Helpers;

public static class BrowserMessage {
    public static string Message(WiThrottleConnections connections, IPAddress host, int port) {
        var sb = new StringBuilder();
        sb.AppendLine("HTTP/1.1 403 Forbidden");
        sb.AppendLine("Content-Type: text/plain");
        sb.AppendLine();
        sb.AppendLine($"The service running on {host}:{port} is a Withrottle service.");
        sb.AppendLine("Please connect to this service using a WiThrottle compatible application");
        sb.AppendLine("such as https://www.withrottle.com");
        sb.AppendLine();
        sb.AppendLine($"+ -------------------- + -------------------- + ------------------------- + ---------- +");
        sb.AppendLine($"| ThrottleName         | HardwareID           | Address                   | Heart Beat |");
        sb.AppendLine($"+ ==================== + ==================== + ========================= + ========== +");
        foreach (var connection in connections.Connections) {
            var con = connection.ThrottleName ?? "Unknown Throttle";
            var hid = connection.HardwareID ?? "No Hardware ID";
            var pid = connection.ConnectionAddress?.ToString() ?? "Unknown IP";

            con = con.Substring(0, Math.Min(con.Length, 20));
            hid = hid.Substring(0, Math.Min(hid.Length, 20));
            pid = pid.Substring(0, Math.Min(pid.Length, 25));

            sb.AppendLine($"| {con,-20} | {hid,-20} | {pid,-25} | {connection.LastHeartbeat:yyyy-MM-dd} |");
            sb.AppendLine($"+ -------------------- + -------------------- + ------------------------- + ---------- +");
        }
        return sb.ToString();
    }
}