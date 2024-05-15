namespace DCCRailway.Common.Configuration;

[Serializable]
public class ServiceSetting {
    public ServiceSetting() { }

    public ServiceSetting(string name, int hostPort) {
        Name     = name;
        HostURL  = "https://localhost";
        HostPort = hostPort;
    }

    public ServiceSetting(string name, int hostPort, string configFile) {
        Name       = name;
        HostURL    = "https://localhost";
        HostPort   = hostPort;
        ConfigFile = configFile;
    }

    public ServiceSetting(string name, string hostURL, int hostPort) {
        Name     = name;
        HostURL  = hostURL;
        HostPort = hostPort;
    }

    public ServiceSetting(string name, string hostURL, int hostPort, string configFile) {
        Name       = name;
        HostURL    = hostURL;
        HostPort   = hostPort;
        ConfigFile = configFile;
    }

    public string  Name       { get; set; }
    public string  HostURL    { get; set; }
    public int     HostPort   { get; set; }
    public string? ConfigFile { get; set; }
    public string  ServiceURL => $"{HostURL}:{HostPort}";
}