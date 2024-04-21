using System.Diagnostics;

namespace DCCRailway.Station.Attributes;

[DebuggerDisplay("Name: {Name}, Manufacturer: {Manufacturer}, Model: {Model}, Version: {Version}")]
[AttributeUsage(AttributeTargets.Class)]
public class ControllerAttribute : Attribute {
    public ControllerAttribute(string name, string? manufacturer = null, string? model = null, string? version = null) {
        Name         = name;
        Manufacturer = manufacturer ?? "";
        Model        = model ?? "";
        Version      = version ?? "";
    }

    public string Name         { get; set; }
    public string Manufacturer { get; set; }
    public string Model        { get; set; }
    public string Version      { get; set; }
}