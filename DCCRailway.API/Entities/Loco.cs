namespace DccRailway.API.Entities;

[Serializable]
public class Loco : IEntity<Guid> {
    public Guid Id { get; set; }
    public string Name { get; set; }

    //public string Description { get; set; }
    //public string Type { get; set; }
    //public string RoadName { get; set; }
    //public string RoadNumber { get; set; }
    //public string Manufacturer { get; set; }
    //public string Model { get; set; }

    //public Decoder Decoder { get; set; }
    //public Parameters Parameters { get; set; }
    public Guid GenerateID() {
        return Guid.NewGuid();
    }
}