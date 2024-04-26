using System.ComponentModel;
using System.Reflection;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Station.Helpers;

public static class ParameterMapper {

    private static BindingFlags LookupPropertyBindingFlags =
        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

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
            if (prop is null) throw new ArgumentException("Provided Property name is not valid: {0}", parameterName);
            var attr = prop.GetCustomAttributes(typeof(ParameterMappableAttribute), false);
            if (attr.Length <= 0) throw new ArgumentException("Provided Property is not supported: {0}", parameterName);

            // Make sure the property exists and it can be written
            if (prop.CanWrite && attr.Length > 0) {
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

    public static bool IsMappableParameter<T>(this T input, string propertyName) where T : IParameterMappable
    {
        var type = input?.GetType(); // Get the type of the current object
        if (type is null) return false;

        var prop = type.GetProperty(propertyName,LookupPropertyBindingFlags); // Try get the property from the given parameterName
        if (prop is null) return false;

        var attr = prop.GetCustomAttributes(typeof(ParameterMappableAttribute), false);
        return attr?.Length >= 0;
    }

    public static Dictionary<string, string?> GetMappableGetParameters<T>(this T input) where T : IParameterMappable {
        var parameters = new Dictionary<string, string?>();
        var type = input?.GetType();
        if (type is not null) {
            var props = type.GetProperties();
            foreach (var prop in props) {
                var attr = prop.GetCustomAttributes(typeof(ParameterMappableAttribute), false);
                if (attr.Length > 0) {
                    var parameterName = prop.Name;
                    var parameterValue = prop.GetValue(input)?.ToString();
                    parameters.Add(parameterName, parameterValue);
                }
            }
        }
        return parameters;
    }
}