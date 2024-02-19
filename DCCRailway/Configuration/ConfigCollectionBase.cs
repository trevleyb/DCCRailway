namespace DCCRailway.Configuration;

public class ConfigCollectionBase<T> : List<T> where T : ConfigBase {
    public T? Find(string name) {
        var element = Find(x => x.Name == name);
        return element ?? null; 
    }
}