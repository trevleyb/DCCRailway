using System.Diagnostics;

namespace DCCRailway.CmdStation.Attributes;

[DebuggerDisplay("Name: {Name}, Description: {Description}, Version: {Version}, Type: {Type}")]
[AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute : Attribute {
    public AdapterAttribute(string? name, AdapterType type = AdapterType.Unknown, string? description = null, string? version = null) {
        Name        = name;
        Type        = type;
        Description = description ?? "";
        Version     = version ?? "";
        Supported   = new List<string>() { "*@*" };
        UnSupported = [];
    }

    public string?      Name        { get; set; }
    public string      Description { get; set; }
    public string      Version     { get; set; }
    public AdapterType Type        { get; set; }

    private List<string> Supported   { get; }
    private List<string> UnSupported { get; }

    // The Supported and Not-Supported properties define the conditions underwhich this command is available.
    // The parameter must be adapter@version where version is optional.
    // If a command is ONLY supported by a particular adapter@version then it should be in the Supported list.
    // If a command is NOT supported by a particular adapter@version then it should be in the NotSupported list.
    // if the supported list is empty/null then ALL adapters and all versions are supported.
    // if the not-supported list if empty/null then no adapters or version are excluded.
    // if either is * then that means ALL (default for the supported list)

    public AdapterAttribute(string? name, AdapterType type, string description, string version, string[]? supported = null, string[]? unSupported = null) : this(name, type, description, version) {
        Supported = supported is null ? [] : supported.ToList();
        UnSupported = unSupported is null ? [] : unSupported.ToList();
    }

    public bool IsSupported(string commandStationName) {
        var parts  = commandStationName.Split('@', 2);
        var station = parts.Length > 0 ? parts[0] : "";
        var version = parts.Length > 1 ? parts[1] : "";
        return IsSupported(station, version);
    }

    public bool IsSupported(string commandStationName, string version) {
        if (string.IsNullOrWhiteSpace(commandStationName)) commandStationName = "*";
        if (string.IsNullOrWhiteSpace(version)) version = "*";
        var item = $"{commandStationName}@{version}";

        if (UnSupported.Contains(item) || UnSupported.Contains(commandStationName) || UnSupported.Contains($"{commandStationName}@*") || UnSupported.Contains($"@{version}") || UnSupported.Contains("*") || UnSupported.Contains("*@*")) {
            return false;
        }

        if (!Supported.Any() || Supported.Contains(item) || Supported.Contains(commandStationName) || Supported.Contains($"{commandStationName}@*") || Supported.Contains($"@{version}") || Supported.Contains("*") || Supported.Contains("*@*")) {
            return true;
        }
        return false;
    }

}

public enum AdapterType {
    Virtual,
    Serial,
    USB,
    Network,
    Unknown
}