using System.ComponentModel;
using System.Reflection;

namespace DCCRailway.Common.Parameters;

public static class ParameterMapper {
    private const BindingFlags LookupPropertyBindingFlags =
        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Public;

    /// <summary>
    ///     This function take a Name/Value pair and looks in the Adapter for those
    ///     properties and if they exist, and if the object data can be converted to
    ///     that type, then it will set the value of that property.
    /// </summary>
    /// <param name="input">The object that supports Property Mapping</param>
    /// <param name="parameterName">The Name of the Property to change</param>
    /// <param name="parameterValue">The value (as a string) to set the property to</param>
    public static void SetMappableParameter<T>(this T input, string parameterName, string parameterValue) where T : IParameterMappable {
        var type = input?.GetType(); // Get the type of the current object
        if (type is not null) {
            var prop = type.GetProperty(parameterName, LookupPropertyBindingFlags); // Try get the property from the given parameterName
            if (prop is not null && prop.CanWrite) {
                var converter = TypeDescriptor.GetConverter(prop.PropertyType); // Get the type converter for the property
                if (converter.IsValid(parameterValue))

                    // Make sure we can convert the parameterValue to the type of the property
                    // Convert the parameterValue to the type of the property and set the value
                    prop.SetValue(input, converter.ConvertFromString(parameterValue), null);
                else
                    throw new InvalidCastException($"Cannot convert \"{parameterValue}\" to type \"{prop.PropertyType}\".");
            } else {
                throw new ArgumentException($"\"{parameterName}\" is not a valid property name in \"{type.Name}\" class.");
            }
        }
    }

    public static bool IsMappableParameter<T>(this T input, string propertyName) where T : IParameterMappable {
        var type = input?.GetType(); // Get the type of the current object
        if (type is null) return false;

        var prop = type.GetProperty(propertyName, LookupPropertyBindingFlags); // Try get the property from the given parameterName
        if (prop is not null)
            if (prop.GetCustomAttribute<ParameterAttribute>() is not null)
                return true;
        return false;
    }

    public static Dictionary<string, ParameterInfo> GetMappableParameters<T>(this T input) where T : IParameterMappable {
        var parameters = new Dictionary<string, ParameterInfo>();
        var type       = input?.GetType();
        if (type is not null) {
            var props = type.GetProperties(LookupPropertyBindingFlags).Where(prop => Attribute.IsDefined(prop, typeof(ParameterAttribute)));
            foreach (var prop in props) {
                parameters.Add(prop.Name, new ParameterInfo(input, prop));
            }
        }
        return parameters;
    }
}