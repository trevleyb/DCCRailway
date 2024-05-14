namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class Adapters : List<Adapter> {
    public Adapter? this[string name] => Find(x => x.Name != null && x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? null;
}