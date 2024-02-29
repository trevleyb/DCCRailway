using System.Text.Json;

namespace DCCRailway.Common.Utilities;

public class JsonSerializerHelper<T> {
    /// <summary>
    ///     Load an instance of class T from a provided filename and throw an exception if the
    ///     file name does not exist.
    /// </summary>
    /// <param name="fileName">The name of the file to load</param>
    /// <returns>An instance of an XML class </returns>
    /// <exception cref="ApplicationException">If it is unable to load the file</exception>
    public static T? Load(string fileName) {
        if (!File.Exists(fileName)) throw new FileNotFoundException($"Unable to access file '{fileName}'");
        try {
            var serializedStr = File.ReadAllText(fileName);

            return JsonSerializer.Deserialize<T>(serializedStr)!;
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to load the configuration file '{fileName}' due to '{ex.Message}'", ex);
        }
    }

    /// <summary>
    ///    Provide a file name for the configuration and save to that file 
    /// </summary>
    /// <param name="collection">The collection to serialise</param>
    /// <param name="fileName">The name of the file to write the data to</param>
    /// <exception cref="ApplicationException">Returns an error if it cannot save</exception>
    public static void Save(T collection, string fileName) {
        if (string.IsNullOrEmpty(fileName)) throw new ApplicationException("You must specify a name for the Configuration File.");

        // Write out the Hierarchy of Configuration Options, from this class, to an XML File
        // -----------------------------------------------------------------------------------
        try {
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
            var serializedStr     = JsonSerializer.Serialize(collection, serializerOptions);
            File.WriteAllText(fileName, serializedStr);
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to save configuration data to '{fileName}' due to '{ex.Message}'");
        }
    }
}