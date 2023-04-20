using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace DCCRailway.Core.Config; 

[XmlRoot(ElementName = "Parameters")]
public class Parameters : List<Parameter> {
    public void Set<T>(string Name, T Value) {
        var parameter = Find(x => x.Name == Name);
        if (parameter == null) parameter = new Parameter {Name = Name};
        parameter!.Value = Value?.ToString() ?? string.Empty;
        Add(parameter);
    }

    public T? Get<T>(string Name) {
        var parameter = Find(x => x.Name == Name);
        return parameter != null ? (T) Convert.ChangeType(parameter.Value, typeof(T), CultureInfo.InvariantCulture) : default;
    }
}