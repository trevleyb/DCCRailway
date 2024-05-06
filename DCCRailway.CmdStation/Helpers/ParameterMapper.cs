using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Controllers;

namespace DCCRailway.CmdStation.Helpers;

public static class ParameterMapper {

    private const BindingFlags LookupPropertyBindingFlags =
        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Public;

    /// <summary>
    /// This function take a Name/Value pair and looks in the Adapter for those
    /// properties and if they exist, and if the object data can be converted to
    /// that type, then it will set the value of that property.
    /// </summary>
    /// <param name="input">The object that supports Property Mapping</param>
    /// <param name="parameterName">The Name of the Property to change</param>
    /// <param name="parameterValue">The value (as a string) to set the property to</param>
    public static void SetMappableParameter<T>(this T input, string parameterName, string parameterValue) where T : IParameterMappable {

        var type = input?.GetType();                  // Get the type of the current object
        if (type is not null) {
            var prop = type.GetProperty(parameterName,LookupPropertyBindingFlags); // Try get the property from the given parameterName
            if (prop is not null && prop.CanWrite) {
                var converter = TypeDescriptor.GetConverter(prop.PropertyType); // Get the type converter for the property
                if (converter.IsValid(parameterValue)) {
                    // Make sure we can convert the parameterValue to the type of the property
                    // Convert the parameterValue to the type of the property and set the value
                    prop.SetValue(input, converter.ConvertFromString(parameterValue), null);
                }
                else {
                    throw new InvalidCastException($"Cannot convert \"{parameterValue}\" to type \"{prop.PropertyType}\".");
                }
            }
            else {
                throw new ArgumentException($"\"{parameterName}\" is not a valid property name in \"{type.Name}\" class.");
            }
        }
    }

    public static bool IsMappableParameter<T>(this T input, string propertyName) where T : IParameterMappable  {
        var type = input?.GetType(); // Get the type of the current object
        if (type is null) return false;

        var prop = type.GetProperty(propertyName,LookupPropertyBindingFlags); // Try get the property from the given parameterName
        if (prop is null) return false;

        return true;
    }

    public static Dictionary<string, string?> GetMappableParameters<T>(this T input) where T : IParameterMappable {
        var parameters = new Dictionary<string, string?>();
        var type = input?.GetType();
        if (type is not null) {
            var props = type.GetProperties(LookupPropertyBindingFlags);
            foreach (var prop in props) {
                var parameterName = prop.Name;
                var parameterValue = prop.GetValue(input)?.ToString();
                parameters.Add(parameterName, parameterValue);
            }
        }
        return parameters;
    }

    /// <summary>
    /// This class builds a collection of what Properties are Valid within an object including the type of data
    /// and if it is an ENUM or other, then what the valid options or ranges are. We should build this up with
    /// the parametermappableatttribute.
    /// </summary>
    public static List<ParameterInfo> GetMappableParameterInfo<T>(this T input) where T : IParameterMappable {

        var parameters = new List<ParameterInfo>();
        var type = input?.GetType();
        if (type is not null) {
            var props = type.GetProperties(LookupPropertyBindingFlags);
            foreach (var prop in props) {
                var pInfo = new ParameterInfo();
                pInfo.Name    = prop.Name;
                pInfo.Type    = prop.PropertyType.ToString();
                pInfo.Value   = prop.GetValue(input)?.ToString();

                if (prop.PropertyType.BaseType?.Name.ToLower() == "enum") {
                    pInfo.Options = GetEnumOptions(prop.PropertyType);
                }
                else {
                    pInfo.Options = prop.PropertyType.Name.ToLower() switch {
                        "byte"   => "0...255",
                        "string" => "String Value",
                        "int32"  => "0...99,999",
                        "bool"   => "true | false",
                        _        => ""
                    };
                }
                parameters.Add(pInfo);
            }
        }
        return parameters;
    }

    private static string? GetEnumOptions(Type prop) {
        var builder = new StringBuilder();
        foreach (var field in prop.GetFields()) {
            if (field.FieldType.BaseType != null && field.FieldType.BaseType.Name.Equals("enum",StringComparison.InvariantCultureIgnoreCase)) {
                if (builder.Length > 0) builder.Append(" | ");
                builder.Append(field.Name);
            }
        }
        return builder.ToString();
    }
}