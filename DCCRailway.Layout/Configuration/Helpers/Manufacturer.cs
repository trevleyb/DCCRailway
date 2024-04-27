namespace DCCRailway.Layout.Configuration.Helpers;

public class Manufacturer  {
    public byte Id { get; set; }
    public string Name { get; set; }

    public Manufacturer(byte id,string name) {
        Id          = id;
        Name        = name;
    }
}