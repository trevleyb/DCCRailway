namespace DCCRailway.Controller.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SupportedAttribute() : Attribute {
    protected SupportedAttribute(string name, string? description = null, string? version = null, string[]? supported = null, string[]? unSupported = null) : this() {
        Name        = name;
        Description = description ?? "";
        Version     = version ?? "";
        Supported   = supported == null ? ["*@*"] : supported.ToList();
        UnSupported = unSupported == null ? [] : unSupported.ToList();
    }

    public string? Name        { get; init; }
    public string  Description { get; init; }
    public string  Version     { get; init; }

    private List<string> Supported   { get; }
    private List<string> UnSupported { get; }

    // The Supported and Not-Supported properties define the conditions underwhich this command is available.
    // The parameter must be adapter@version where version is optional.
    // If a command is ONLY supported by a particular adapter@version then it should be in the Supported list.
    // If a command is NOT supported by a particular adapter@version then it should be in the NotSupported list.
    // if the supported list is empty/null then ALL adapters and all versions are supported.
    // if the not-supported list if empty/null then no adapters or version are excluded.
    // if either is * then that means ALL (default for the supported list)

    public bool IsSupported(string commandStationName) {
        var parts   = commandStationName.Split('@', 2);
        var station = parts.Length > 0 ? parts[0] : "";
        var version = parts.Length > 1 ? parts[1] : "";
        return IsSupported(station, version);
    }

    public bool IsSupported(string commandStationName, string version) {
        if (string.IsNullOrWhiteSpace(commandStationName)) commandStationName = "*";
        if (string.IsNullOrWhiteSpace(version)) version                       = "*";
        var item                                                              = $"{commandStationName}@{version}";

        if (UnSupported.Contains(item) || UnSupported.Contains(commandStationName) || UnSupported.Contains($"{commandStationName}@*") || UnSupported.Contains($"@{version}") || UnSupported.Contains("*") || UnSupported.Contains("*@*")) return false;

        if (!Supported.Any() || Supported.Contains(item) || Supported.Contains(commandStationName) || Supported.Contains($"{commandStationName}@*") || Supported.Contains($"@{version}") || Supported.Contains("*") || Supported.Contains("*@*")) return true;
        return false;
    }
}