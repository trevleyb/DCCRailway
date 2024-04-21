namespace DCCRailway.Layout.Configuration.Entities.Base;

public interface IEntity<TKey> {
    TKey Id { get; set; }
    string Name { get; set; }
}