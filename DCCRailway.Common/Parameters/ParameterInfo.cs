using System.Reflection;
using System.Text;

namespace DCCRailway.Common.Parameters;

public class ParameterInfo {
    public string Description;
    public string Name;
    public string Options;
    public string Type;
    public string Value;

    public ParameterInfo(object? field, PropertyInfo propertyInfo) {
        var attribute = propertyInfo.GetCustomAttribute<ParameterAttribute>();
        Name  = propertyInfo.Name;
        Type  = propertyInfo.GetType().ToString();
        Value = field == null ? string.Empty : propertyInfo.GetValue(field)?.ToString() ?? string.Empty;

        if (attribute != null) {
            Options     = (string.IsNullOrEmpty(attribute.Options) ? attribute.Options : GetAvailableOptions(propertyInfo)) ?? string.Empty;
            Description = attribute.Description ?? string.Empty;
        } else {
            Options = GetAvailableOptions(propertyInfo);
        }
    }

    private string GetAvailableOptions(PropertyInfo prop) {
        if (prop.PropertyType.BaseType?.Name.ToLower() == "enum") {
            return GetEnumOptions(prop.PropertyType);
        }

        return prop.PropertyType.Name.ToLower() switch {
            "byte"   => "0...255",
            "string" => "String Value",
            "int32"  => "0...99,999",
            "bool"   => "true | false",
            _        => ""
        };

        string GetEnumOptions(Type property) {
            var builder = new StringBuilder();

            foreach (var field in property.GetFields()) {
                if (field.FieldType.BaseType != null && field.FieldType.BaseType.Name.Equals("enum", StringComparison.InvariantCultureIgnoreCase)) {
                    if (builder.Length > 0) builder.Append(" | ");
                    builder.Append(field.Name);
                }
            }

            return builder.ToString();
        }
    }
}