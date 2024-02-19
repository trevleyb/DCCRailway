using System.Globalization;
using System.Xml.Serialization;

namespace DCCRailway.Configuration;

public class Controller : ConfigBase {
    public Adapter Adapter { get; set; }
}