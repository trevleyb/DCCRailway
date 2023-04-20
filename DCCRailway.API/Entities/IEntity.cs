namespace DccRailway.API.Entities; 

public interface IEntity<TID> {
    public TID Id { get; set; }
    public string Name { get; set; }
    public TID GenerateID();
}