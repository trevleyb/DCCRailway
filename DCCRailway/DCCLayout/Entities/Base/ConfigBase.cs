namespace DCCRailway.DCCLayout.Entities.Base;

public abstract class ConfigBase {
    public string     Name        { get; set; }
    public string     Description { get; set; }
    public Parameters Parameters  { get; set; } = new Parameters();
}