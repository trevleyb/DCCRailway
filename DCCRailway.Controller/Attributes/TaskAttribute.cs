using System.Diagnostics;

namespace DCCRailway.Controller.Attributes;

[DebuggerDisplay("Name: {Name}, Description: {Description}, Version: {Version}")]
[AttributeUsage(AttributeTargets.Class)]
public class TaskAttribute : SupportedAttribute {
    public TaskAttribute(string name, string? description = null, string? version = null, string[]? supported = null, string[]? unSupported = null) : base(name, description, version, unSupported) { }
}