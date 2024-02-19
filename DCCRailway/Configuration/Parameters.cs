using System.Globalization;
using System.Xml.Serialization;

namespace DCCRailway.Configuration;
public class Parameters : List<Parameter> {

    public void Delete(string name) {
        ArgumentNullException.ThrowIfNull(name);
        var parameter = Find(x => x.Name.Equals(name));
        if (parameter != null) this.Remove(parameter);
    }
    
    public void Add(string name, object value) {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);
        if (Find(x => x.Name.Equals(name)) != null) throw new ArgumentException($"Parameter '{name}' already exists");
        Add(new Parameter ( name, value ));
    }
    
    public void Add<T>(string name, T value) {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);
        Add(new Parameter ( name, value ));
    }

    public void Set<T>(string name, object value) {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);
        var parameter = Find(x => x.Name.Equals(name)) ?? new Parameter { Name = name };
        parameter.Set(name, value);
        Add(parameter);
    }

    public void Set(string name, object value) {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);
        var parameter = Find(x => x.Name.Equals(name)) ?? new Parameter { Name = name };
        parameter.Set(name, value);
        Add(parameter);
    }

    public object? Get(string name) {
        ArgumentNullException.ThrowIfNull(name);
        var parameter = Find(x => x.Name.Equals(name));
        if (parameter != null) return parameter.Get();
        return default;
    }

    public T? Get<T>(string name) {
        ArgumentNullException.ThrowIfNull(name);
        var parameter = Find(x => x.Name.Equals(name));
        if (parameter != null) return parameter.Get<T>();
        return default;
    }
}