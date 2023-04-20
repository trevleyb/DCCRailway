using System;

namespace DCCRailway.Core.Systems.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class SystemNameAttribute : Attribute {
    public string Manufacturer { get; set; }
    public string Name { get; set; }
}