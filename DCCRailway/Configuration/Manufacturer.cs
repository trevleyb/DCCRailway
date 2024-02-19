namespace DCCRailway.Configuration;
public class Manufacturer : Base.ConfigBase {

    public byte Identifier { get; set; }
    public Manufacturer(string name, byte identifier) {
        Name        = name;
        Description = name;
        Identifier  = identifier;
    }
}