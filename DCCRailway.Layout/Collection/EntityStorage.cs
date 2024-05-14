using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Text.Json;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Collection;

public class EntityStorage<TEntity> : ConcurrentDictionary<string,TEntity>
    where TEntity : LayoutEntity {


    /// <summary>
    ///     LoadFile an instance of class T from a provided filename and throw an exception if the
    ///     file name does not exist.
    /// </summary>
    /// <param name="fileName">The name of the file to load</param>
    /// <returns>An instance of an XML class </returns>
    /// <exception cref="ApplicationException">If it is unable to load the file</exception>
    protected void Load(string fileName) {
        Clear();
        if (!File.Exists(fileName)) return;
        try {
            var serializedStr = File.ReadAllText(fileName);
            var serializerOptions = new JsonSerializerOptions {
                WriteIndented = true
            };
            var collection = JsonSerializer.Deserialize<Dictionary<string, TEntity>>(serializedStr, serializerOptions) ?? [];
            foreach (var keyPair in collection) TryAdd(keyPair.Key, keyPair.Value);
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to load the configuration file '{fileName}' due to '{ex.Message}'", ex);
        }
    }

    /// <summary>
    ///    Provide a file name for the configuration and save to that file
    /// </summary>
    /// <param name="fileName">The name of the file to write the data to</param>
    /// <exception cref="ApplicationException">Returns an error if it cannot save</exception>
    protected void Save(string fileName) {
        if (string.IsNullOrEmpty(fileName)) throw new ApplicationException("You must specify a name for the Configuration File.");

        // Write out the Hierarchy of Configuration Options, from this class, to an XML File
        // -----------------------------------------------------------------------------------
        try {
            var serializerOptions   = new JsonSerializerOptions {
                    WriteIndented = true
            };
            var serializedStr = JsonSerializer.Serialize(this.ToFrozenDictionary(), serializerOptions);
            File.WriteAllText(fileName, serializedStr);
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to save configuration data to '{fileName}' due to '{ex.Message}'");
        }
    }
}