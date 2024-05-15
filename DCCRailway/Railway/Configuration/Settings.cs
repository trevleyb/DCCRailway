using System.Text.Json;
using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Throttles.WiThrottle;
using Controller = DCCRailway.Railway.Configuration.Entities.Controller;

namespace DCCRailway.Railway.Configuration;

[Serializable]
public class Settings : JsonSerializerHelper<Settings> {
    public string         Name                  { get; set; } = "DCCRailway";
    public string         Description           { get; set; } = "";

    public Entities.Controller      Controller  { get; set; } = new();
    public Entities.Parameters      Parameters  { get; set; } = new();
    public Entities.Entities        Entities    { get; set; } = new();
    public WiThrottlePreferences    WiThrottle  { get; set; } = new();

    public string PathName { get; set; }
    public string FileName => $"{Name}.Settings.json";
    public string FullName => Path.Combine(PathName ?? "", FileName);

    public void Save() => SaveFile(this,FullName);
    public void Save(string? fullName) => SaveFile(this,fullName ?? FullName);
    public Settings? Load() => LoadFile(FullName);
    public Settings? Load(string? fullName) => LoadFile(fullName ?? FullName);
}