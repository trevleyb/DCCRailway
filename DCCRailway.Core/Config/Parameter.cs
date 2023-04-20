using System;
using System.Globalization;
using System.Xml.Serialization;

namespace DCCRailway.Core.Config; 

[XmlRoot(ElementName = "Parameter")]
public class Parameter {
    [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }

    [XmlAttribute(AttributeName = "Value")]
    public string Value { get; set; }

    public void Set<T>(string Name, T Value) {
        this.Name = Name;
        this.Value = Value?.ToString() ?? string.Empty;
    }

    public T Get<T>() {
        return (T) Convert.ChangeType(Value, typeof(T), CultureInfo.InvariantCulture);
    }

    public new string ToString() {
        return $"{Name}='{Value}'";
    }
}