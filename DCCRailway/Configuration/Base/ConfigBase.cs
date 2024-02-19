namespace DCCRailway.Configuration.Base;

public abstract class ConfigBase {
    public string     Identifier  { get; set; }
    public string     Name        { get; set; }
    public string     Description { get; set; }
    public Parameters Parameters  { get; set; } = new Parameters();
}