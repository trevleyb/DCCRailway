namespace DCCRailway.Layout.Configuration;

[Serializable]
public class Settings {
    public string Name        { get; set; } = "DCCRailway";
    public string Description { get; set; } = "";
    public string PathName    { get; set; }

    public Controller   Controller { get; set; } = new();
    public Parameters   Parameters { get; set; } = new();
    public WiThrottlePrefs WiThrottle { get; set; } = new();

}