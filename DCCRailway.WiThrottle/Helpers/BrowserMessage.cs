using System.Net;
using System.Text;
using DCCRailway.WiThrottle.Server;

namespace DCCRailway.WiThrottle.Helpers;

public static class BrowserMessage {
    public static string Message(Connections connections, IPAddress host, int port) {
        var sb = new StringBuilder();
        sb.AppendLine("HTTP/1.1 403 Forbidden");
        sb.AppendLine("Content-Type: text/plain");
        sb.AppendLine();
        sb.AppendLine($"The service running on {host}:{port} is a Withrottle service.");
        sb.AppendLine("Please connect to this service using a WiThrottle compatible application");
        sb.AppendLine("such as https://www.withrottle.com");
        sb.AppendLine();
        sb.AppendLine($"+======================+======================================+===========================+=====================+=====+");
        sb.AppendLine($"| ThrottleName         | HardwareID                           | Address                   | Heart Beat          | Act |");
        sb.AppendLine($"+======================+======================================+===========================+=====================+=====+");

        foreach (var connection in connections.ActiveConnections) {
            var con = connection.ThrottleName ?? "Unknown Throttle";
            var hid = connection.HardwareID ?? "No Hardware ID";
            var pid = connection.ConnectionAddress?.ToString() ?? "Unknown IP";

            con = con.Substring(0, Math.Min(con.Length, 20));
            hid = hid.Substring(0, Math.Min(hid.Length, 36));
            pid = pid.Substring(0, Math.Min(pid.Length, 25));

            sb.AppendLine($"| {con,-20} | {hid,-36} | {pid,-25} | {connection.LastHeartbeat:yyyy-MM-dd hh:mm:ss} | {(connection.IsActive ? "Yes" : "No ")} |");
            sb.AppendLine($"+----------------------+--------------------------------------+---------------------------+---------------------+-----+");
        }

        sb.AppendLine();
        sb.AppendLine($"+======================+======================================+=======+");
        sb.AppendLine($"| DCC Address          | Owned By                             | Group |");
        sb.AppendLine($"+======================+======================================+=======+");

        foreach (var entry in connections.Assignments.AssignedAddresses) {
            var add = entry.Address.ToString();
            var own = entry.Connection.ThrottleName;
            var grp = entry.Group.ToString();

            add = add.Substring(0, Math.Min(add.Length, 20));
            own = own?.Substring(0, Math.Min(own.Length, 36)) ?? "No Owner";
            grp = grp?.Substring(0, Math.Min(grp.Length, 5)) ?? "None ";
            sb.AppendLine($"| {add,-20} | {own,-36} | {grp,-5} |");
            sb.AppendLine($"+----------------------+--------------------------------------+-------+");
        }

        return sb.ToString();
    }
}