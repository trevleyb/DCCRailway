using System.Diagnostics;

namespace DCCRailway.Controller.Attributes;

[DebuggerDisplay("Name: {Name}, Description: {Description}, Version: {Version}")]
[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : SupportedAttribute {
    public CommandAttribute(string name, string? description = "", string? version = "", string[]? supported = null, string[]? unSupported = null) : base(name, description, version, supported,unSupported) { }
}