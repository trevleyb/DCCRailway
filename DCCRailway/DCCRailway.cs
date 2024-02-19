using System.Xml;
using System.Xml.Serialization;
using DCCRailway.Configuration;
using DCCRailway.Configuration.Entities;
using DCCRailway.Utilities;

namespace DCCRailway;

/// <summary>
/// System: Represents the configuration and status of an operating DCC Model Train System. These classes are
/// used to serialize and deserialize the configuration of the system.
/// </summary>

[XmlRoot(ElementName = "System")]
public class DCCRailway {

    public DCCRailway() {}

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
    public static DCCRailway? Load(string name) => JsonSerializerHelper<DCCRailway>.Load(name);
    public        void       Save(string name) => JsonSerializerHelper<DCCRailway>.Save(this, name);
    public string Save() {
        var fileName = System.IO.Path.ChangeExtension(Name, ".json");
        JsonSerializerHelper<DCCRailway>.Save(this, fileName);
        return fileName;
    }

    #endregion
}