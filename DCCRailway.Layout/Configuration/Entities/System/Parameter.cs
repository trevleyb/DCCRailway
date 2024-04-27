using System.Globalization;
using System.IO.Ports;
using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.Events;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Parameter: PropertyChangeBase, IEntity {

    public Guid     Id { get; set; }
    public string   Name    { get; set; }
    public string   Value   { get; set; }
    public string?  ObjType { get; set; }

    public Parameter() : this (Guid.NewGuid()) { }
    public Parameter(Guid guid) => Id = guid;

    public Parameter(string name, object value) => Set(name, value);

    public void Set(string name, object value) {
        Name    = name;
        ObjType = value.GetType().FullName;
        Value   = (string)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
    }

    public T? Get<T>() => (T?)Convert.ChangeType(Get(), typeof(T));

    public object Get() {
        try {
            if (ObjType is not null)
                return ObjType switch {
                    "System.String"             => Value,
                    "System.Int32"              => Convert.ToInt32(Value, CultureInfo.InvariantCulture),
                    "System.Int64"              => Convert.ToInt64(Value, CultureInfo.InvariantCulture),
                    "System.Double"             => Convert.ToDouble(Value, CultureInfo.InvariantCulture),
                    "System.Single"             => Convert.ToSingle(Value, CultureInfo.InvariantCulture),
                    "System.Boolean"            => Convert.ToBoolean(Value, CultureInfo.InvariantCulture),
                    "System.IO.Ports.Parity"    => (Parity)Enum.Parse(typeof(Parity), Value),
                    "System.IO.Ports.StopBits"  => (StopBits)Enum.Parse(typeof(StopBits), Value),
                    "System.IO.Ports.Handshake" => (Handshake)Enum.Parse(typeof(Handshake), Value),
                    "System.IO.Ports.DataBits"  => Convert.ToInt32(Value, CultureInfo.InvariantCulture),
                    _                           => Convert.ChangeType(Value, Type.GetType(ObjType) ?? typeof(string), CultureInfo.InvariantCulture)
                };
        }
        catch {
            return Convert.ChangeType(Value, typeof(string), CultureInfo.InvariantCulture);
        }

        return Value;
    }

    public new string ToString() => $"{Name}='{Value}'";
}