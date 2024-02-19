using System.Xml.Serialization;
using DCCRailway.Configuration.Entities;
using DCCRailway.Utilities;

namespace DCCRailway.Configuration;

/// <summary>
/// System: Represents the configuration and status of an operating DCC Model Train System. These classes are
/// used to serialize and deserialize the configuration of the system.
/// </summary>

[XmlRoot(ElementName = "System")]
public class DCCRailwayConfig {

    public DCCRailwayConfig() {}

    public string      Name        { get; set; } = "My Layout";
    public string      Description { get; set; } = ""; 
    public Controllers Controllers { get; set; } = new Controllers();
    public Parameters  Parameters  { get; set; } = new Parameters();
    public Locomotives Locomotives { get; set; } = new Locomotives();
    public Turnouts    Turnouts    { get; set; } = new Turnouts();
    public Signals     Signals     { get; set; } = new Signals();
    public Sensors     Sensors     { get; set; } = new Sensors();
    public Accessories Accessories { get; set; } = new Accessories();
    public Blocks      Blocks      { get; set; } = new Blocks();

    #region Load and Save Functions
    public static DCCRailwayConfig? Load(string name) => JsonSerializerHelper<DCCRailwayConfig>.Load(name);
    public        void       Save(string name) => JsonSerializerHelper<DCCRailwayConfig>.Save(this, name);
    public string Save() {
        var fileName = System.IO.Path.ChangeExtension(Name, ".json");
        JsonSerializerHelper<DCCRailwayConfig>.Save(this, fileName);
        return fileName;
    }
    #endregion
}