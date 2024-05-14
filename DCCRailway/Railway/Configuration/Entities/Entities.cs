namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class Entities : List<Entity> {
    public Entity? this[string name] => Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? null;
}