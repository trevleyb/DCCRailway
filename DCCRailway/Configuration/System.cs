using System.Xml.Serialization;
using DCCRailway.Utilities;

namespace DCCRailway.Configuration;

/// <summary>
/// System: Represents the configuration and status of an operating DCC Model Train System. These classes are
/// used to serialize and deserialize the configuration of the system.
/// </summary>

[XmlRoot(ElementName = "System")]
public class System {
    
    public System() { }

    public string      Name        { get; set; } = "Unknown";
    public string      Description { get; set; } = "Unknown"; 
    public Controllers Controllers { get; set; } = new Controllers();
    public Parameters  Parameters  { get; set; } = new Parameters();
    //public Locomotives Locomotives { get; set; } = new Locomotives();
    //public Turnouts    Turnouts    { get; set; } = new Turnouts();
    //public Signals     Signals     { get; set; } = new Signals();
    //public Sensors     Sensors     { get; set; } = new Sensors();
    //public Accessories Accessories { get; set; } = new Accessories();
    //public Blocks      Blocks      { get; set; } = new Blocks();

    #region Load and Save Functions
    public static System? Load(string name) => JsonSerializerHelper<System>.Load(name);
    public        void    Save(string name) => JsonSerializerHelper<System>.Save(this, name);
    #endregion
    
}