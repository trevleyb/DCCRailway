using System.Text.RegularExpressions;
using DCCRailway.WiThrottle.Client;

namespace DCCRailway.WiThrottle.ServiceHelper;

/// <summary>
/// Record containing the found information for a service
/// </summary>
public class ServiceInfo {
    public ServiceInfo(string name, string address, int port) : this(name, new ClientInfo(address, port)) { }

    public ServiceInfo(string name, ClientInfo clientInfo) {
        // Remove the trailing TCP part from the name of the service if it exists
        // Then make sure there are no Unicode identifiers in the string. If there are 
        // then remove then and replace with the correct ASCII character.
        // ---------------------------------------------------------------------------------
        if (!string.IsNullOrEmpty(name) && name.Contains("._")) {
            name = name.Substring(0, name.IndexOf("._", StringComparison.Ordinal));
        }

        Name       = ConvertOctalToAscii(name);
        ClientInfo = clientInfo;
    }

    public string     Name       { get; set; }
    public ClientInfo ClientInfo { get; set; }

    private string ConvertOctalToAscii(string input) {
        if (string.IsNullOrEmpty(input)) return "";
        return Regex.Replace(input, @"\\(?<Decimal>[0-9]{1,3})", match => {
            var decimalNumber = match.Groups["Decimal"].Value;
            var number        = int.Parse(decimalNumber);
            return ((char)number).ToString();
        });
    }
}