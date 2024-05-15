using System.Diagnostics;

namespace DCCRailway.Controller.Attributes;

[DebuggerDisplay("Name: {Name}, Description: {Description}, Version: {Version}, Type: {Type}"), AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute : SupportedAttribute {
    public AdapterAttribute(string name, AdapterType type = AdapterType.Unknown, string? description = null, string? version = null, string[]? supported = null, string[]? unSupported = null) : base(name, description, version, supported, unSupported) => Type = type;
    public AdapterType Type { get; set; }
}

public enum AdapterType {
    Virtual,
    Serial,
    USB,
    Network,
    Unknown
}