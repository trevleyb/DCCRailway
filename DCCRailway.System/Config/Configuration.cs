using System.Xml.Serialization;

namespace DCCRailway.System.Config;

/// <summary>
///     Represents a self-contained set of configuration items for the running
///     of a DCC Train Layout. This will include what SYSTEMS to instantiate,
///     what Adapaters to attach, and what "things" can be operated on within the
///     system such as Locos, Accessories, Signals, Aspects and Turnouts
/// </summary>
[XmlRoot(ElementName = "DCCTrainCommander")]
public class Configuration : ConfigSerializer<Configuration> {
    public Configuration() {
        Systems     = new List<System>();
        Locos       = new List<Loco>();
        Accessories = new List<Accessory>();
        Signals     = new List<Signal>();
        Turnouts    = new List<Turnout>();
        Sensors     = new List<Sensor>();
        Blocks      = new List<Block>();
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    #region Elements and Sttributes Saved as part of this configuration file
    [XmlArray(ElementName = "Systems")]
    public List<System> Systems { get; set; }

    [XmlArray(ElementName = "Locos")]
    public List<Loco> Locos { get; set; }

    [XmlArray(ElementName = "Accessories")]
    public List<Accessory> Accessories { get; set; }

    [XmlArray(ElementName = "Signals")]
    public List<Signal> Signals { get; set; }

    [XmlArray(ElementName = "Turnouts")]
    public List<Turnout> Turnouts { get; set; }

    [XmlArray(ElementName = "Sensors")]
    public List<Sensor> Sensors { get; set; }

    [XmlArray(ElementName = "Blocks")]
    public List<Block> Blocks { get; set; }
    #endregion

    #region Load and Save Functions
    /// <summary>
    ///     Load an instance of this type and store the name of the file that was used to load
    ///     it into the instance itself.
    /// </summary>
    /// <param name="Name">The name of the file to load</param>
    /// <returns></returns>
    public new static Configuration? Load(string Name) {
        var config                      = ConfigSerializer<Configuration>.Load(Name);
        if (config != null) config.Name = Name;

        return config;
    }

    /// <summary>
    ///     Write the contents of THIS instance to the specified file
    /// </summary>
    /// <exception cref="ApplicationException"></exception>
    public void Save() => Save(this, Name);
    #endregion
}