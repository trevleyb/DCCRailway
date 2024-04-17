using System.Xml.Serialization;
using DCCRailway.Common.Utilities;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout;
/// <summary>
/// System: Represents the configuration and status of an operating DCC Model Train System. These classes are
/// used to serialize and deserialize the configuration of the system.
/// </summary>
[XmlRoot(ElementName = "System")]
public class DCCRailwayConfig : IDCCRailwayConfig {
    public DCCRailwayConfig() { }

    public string      Name        { get; set; } = "My Layout";
    public string      Description { get; set; } = "";
    public Controllers Controllers { get; set; } = [];
    public Parameters  Parameters  { get; set; } = [];
    public Locomotives Locomotives { get; set; } = [];
    public Turnouts    Turnouts    { get; set; } = [];
    public Signals     Signals     { get; set; } = [];
    public Sensors     Sensors     { get; set; } = [];
    public Accessories Accessories { get; set; } = [];
    public Blocks      Blocks      { get; set; } = [];

    #region Load and Save Functions
    public static IDCCRailwayConfig? Load(string? name) => JsonSerializerHelper<DCCRailwayConfig>.Load(name);
    public        void               Save(string? name) => JsonSerializerHelper<DCCRailwayConfig>.Save(this, name);

    public string? Save() {
        var fileName = Path.ChangeExtension(Name, ".json");
        JsonSerializerHelper<DCCRailwayConfig>.Save(this, fileName);
        return fileName;
    }
    #endregion
}