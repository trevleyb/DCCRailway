namespace DCCRailway.Configuration;
public class Manufacturer : Base.ConfigBase {

    public Manufacturer(string name, string identifier) {
        Name        = name;
        Description = name;
        Identifier  = identifier;
    }

    public Manufacturer(string name, int identifier) : this(name, identifier.ToString()) { }
}