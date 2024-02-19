using System.Xml.Serialization;

namespace DCCRailway.Configuration;

public abstract class ConfigBase {
    public byte       Identifier  { get; set; }
    public string     Name        { get; set; }
    public string     Description { get; set; }
    public Parameters Parameters  { get; set; } = new Parameters();
}