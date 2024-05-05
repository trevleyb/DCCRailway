using System.Net;
using System.Text.Json.Serialization;

namespace DCCRailway.Layout.Configuration.Helpers;

[Serializable]
public class ServiceSetting {

    public ServiceSetting() {}

    public ServiceSetting(string name, int hostPort) {
        Name     = name;
        HostURL  = Dns.GetHostName();
        HostPort = hostPort;
    }

    public ServiceSetting(string name, string hostURL, int hostPort) {
        Name     = name;
        HostURL  = hostURL;
        HostPort = hostPort;
    }

    public string   Name        { get; set; }
    public string   HostURL     { get; set; }
    public int      HostPort    { get; set; }
    public string   ServiceURL => $"{HostURL}/:{HostPort}";
}