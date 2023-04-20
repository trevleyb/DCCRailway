using System;

namespace DCCRailway.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute {
    public CommandAttribute(string name, string? description = null, string? version = null) {
        Name = name;
        Version = version ?? "";
        Description = description ?? Name;
    }

    public string Description { get; set; }
    public string Version { get; set; }
    public string Name { get; set; }
}