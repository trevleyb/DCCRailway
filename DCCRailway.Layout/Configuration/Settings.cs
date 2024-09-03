using System.Text.Json.Serialization;

namespace DCCRailway.Layout.Configuration;

[Serializable]
public class Settings {
    public string Name        { get; set; } = "DCCRailway";
    public string Description { get; set; } = "";
    public string PathName    { get; set; }

    public Controller      Controller { get; set; } = new();
    public Controller      Analyser   { get; set; } = new();
    public WiThrottlePrefs WiThrottle { get; set; } = new();

    public int WebApiPort { get; set; } = 8081;
    public int WebAppPort { get; set; } = 8080;

    [JsonIgnore] public FastClock FastClock { get; set; } = new();
}