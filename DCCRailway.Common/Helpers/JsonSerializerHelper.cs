using System.Text.Json;
using System.Text.Json.Serialization;

namespace DCCRailway.Common.Helpers;

public static class JsonSerializerHelper<T> {
    /// <summary>
    ///     LoadFile an instance of class T from a provided filename and throw an exception if the
    ///     file name does not exist.
    /// </summary>
    /// <param name="fileName">The name of the file to load</param>
    /// <returns>An instance of an XML class </returns>
    /// <exception cref="ApplicationException">If it is unable to load the file</exception>
    public static T? LoadFile(string? fileName) {
        if (!File.Exists(fileName)) return default;
        try {
            var serializedStr     = File.ReadAllText(fileName);
            var serializerOptions = JsonSerializerHelper.Options;
            return JsonSerializer.Deserialize<T>(serializedStr, serializerOptions)!;
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to load the configuration file '{fileName}' due to '{ex.Message}'",
                                           ex);
        }
    }

    /// <summary>
    ///     Provide a file name for the configuration and save to that file
    /// </summary>
    /// <param name="entity">The collection to serialise</param>
    /// <param name="fileName">The name of the file to write the data to</param>
    /// <exception cref="ApplicationException">Returns an error if it cannot save</exception>
    public static void SaveFile(T entity, string? fileName) {
        if (string.IsNullOrEmpty(fileName))
            throw new ApplicationException("You must specify a name for the Configuration File.");

        // Write out the Hierarchy of Configuration Options, from this class, to an XML File
        // -----------------------------------------------------------------------------------
        try {
            var serializerOptions = JsonSerializerHelper.Options;
            var serializedStr     = JsonSerializer.Serialize(entity, serializerOptions);
            File.WriteAllText(fileName, serializedStr);
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to save configuration data to '{fileName}' due to '{ex.Message}'");
        }
    }
}

public static class JsonSerializerHelper {
    public static JsonSerializerOptions Options => new() {
        WriteIndented               = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition      = JsonIgnoreCondition.WhenWritingNull,
        Converters                  = { new JsonStringEnumConverter() }
    };
}