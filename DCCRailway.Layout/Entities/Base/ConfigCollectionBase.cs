namespace DCCRailway.Layout.Entities.Base;

public class ConfigCollectionBase<T> : List<T> where T : ConfigBase {
    public T? this[string name] => Find(name);
    public T? Find(string name) {
        var element = Find(x => x.Name == name);
        return element ?? null; 
    }
}