using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Throttles.WiThrottle;

namespace DCCRailway.Railway.Configuration;

[Serializable]
public class Settings {
    public string Name        { get; set; } = "DCCRailway";
    public string Description { get; set; } = "";
    public string PathName    { get; set; }

    public Entities.Controller   Controller { get; set; } = new();
    public Entities.Parameters   Parameters { get; set; } = new();
    public WiThrottlePreferences WiThrottle { get; set; } = new();

}