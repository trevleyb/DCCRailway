using System.Diagnostics;

namespace DCCRailway.Controller.Attributes;

[DebuggerDisplay("Name: {Name}")]
[AttributeUsage(AttributeTargets.Property)]
public class ParameterMappableAttribute : Attribute {
    public string Name { get; set; }
    public Type   Type { get; set; }
}