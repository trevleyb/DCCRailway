namespace DCCRailway.Layout.Controllers;

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