using DCCRailway.Configuration.Entities.Base;

namespace DCCRailway.Configuration;
public class Manufacturer : ConfigBase {

    public byte Identifier { get; set; }
    public Manufacturer(string name, byte identifier) {
        Name        = name;
        Description = name;
        Identifier  = identifier;
    }
}