namespace DCCRailway.System.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute : Attribute {
    public AdapterAttribute(string name, AdapterType type = AdapterType.Unknown, string? description = null, string? version = null) {
        Name = name;
        Type = type;
        Description = description ?? "";
        Version = version ?? "";
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public AdapterType Type { get; set; }
    public string Version { get; set; }
}

public enum AdapterType {
    Virtual,
    Serial,
    USB,
    Network,
    Unknown
}