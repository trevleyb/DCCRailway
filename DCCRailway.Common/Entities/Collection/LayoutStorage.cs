using System.Text.Json;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Helpers;
using ILogger = Serilog.ILogger;

namespace DCCRailway.Common.Entities.Collection;

/// <summary>
/// This is a more complex JSON serializer designed to seriaoise any properties in the
/// main collection class, and the contents of the dictionary that the class is derived from
/// </summary>
public static class LayoutStorage {
    /// <summary>
    /// Load the file with the specified name and deserialize its contents into an instance of the provided class.
    /// If the file does not exist, an empty instance of the class will be returned.
    /// If the deserialization fails, the original file will be backed up and an empty instance of the class will be returned.
    /// </summary>
    /// <typeparam name="TClass">The type of the class to deserialize the file into.</typeparam>
    /// <typeparam name="TEntity">The type of the entities in the class.</typeparam>
    /// <param name="logger">logger reference</param>
    /// <param name="fileName">The name of the file to load.</param>
    /// <returns>Returns an instance of the class deserialized from the file or an empty instance of the class if the file does not exist or deserialization fails.</returns>
    public static TClass? LoadFile<TClass, TEntity>(ILogger logger, string? fileName) where TEntity : LayoutEntity where TClass : LayoutRepository<TEntity>, new() {
        if (!File.Exists(fileName)) return new TClass();

        try {
            var jsonString = File.ReadAllText(fileName);
            return DeserializeLayout<TClass, TEntity>(jsonString);
        } catch (Exception ex) {
            var backupFile = Path.ChangeExtension(fileName, null); // remove the old extension
            File.Move(fileName, backupFile + ".Backup-{DateTime.Now:yyMMddHHmmss}.json");
            logger.Warning("Could not load the repository file {0} due to {1}. File has been backed up to {2} and an emplty repository created.", fileName, ex.Message, backupFile);
            return new TClass();
        }
    }

    /// <summary>
    /// Saves the provided instance of the class to a file specified by the fileName parameter.
    /// The contents of the class instance will be serialized and saved in JSON format to the file.
    /// If the fileName parameter is null or empty, an ApplicationException will be thrown.
    /// If an exception occurs during serialization or file writing, an ApplicationException will be thrown.
    /// </summary>
    /// <typeparam name="TClass">The type of the class to serialize and save.</typeparam>
    /// <typeparam name="TEntity">The type of entities in the class.</typeparam>
    /// <param name="logger">Logger reference</param>
    /// <param name="entityClass">The instance of the class to save.</param>
    /// <param name="fileName">The name of the file to save.</param>
    /// <exception cref="ApplicationException">Thrown when fileName is null or empty or when an exception occurs during serialization or file writing.</exception>
    public static void SaveFile<TClass, TEntity>(ILogger logger, TClass entityClass, string? fileName) where TEntity : LayoutEntity where TClass : LayoutRepository<TEntity> {
        if (string.IsNullOrEmpty(fileName)) {
            throw new ApplicationException("You must specify a name for the Configuration File.");
        }

        try {
            var jsonString = SerializeLayout<TClass, TEntity>(entityClass);
            File.WriteAllText(fileName, jsonString);
        } catch (Exception ex) {
            logger.Warning("Could not save the repository {0} to file {1} due to {2}.", typeof(TClass).Name, fileName, ex.Message);
        }
    }

    /// <summary>
    /// Deserialize the provided JSON string into an instance of the specified class, ensuring it implements the necessary interface and is of the correct type for the entities.
    /// </summary>
    /// <typeparam name="TClass">The type of the class to deserialize the JSON into.</typeparam>
    /// <typeparam name="TEntity">The type of the entities in the class.</typeparam>
    /// <param name="jsonString">The JSON string to deserialize.</param>
    /// <returns>
    /// Returns an instance of the specified class deserialized from the JSON string.
    /// </returns>
    /// <exception cref="ApplicationException">Thrown if it fails to deserialize the JSON due to an error.</exception>
    /// <remarks>
    /// The specified class must implement the <see cref="ILayoutRepository{TEntity}"/> interface and have the <see cref="LayoutEntity"/> as the base class for the entities in the class.
    /// </remarks>
    public static TClass? DeserializeLayout<TClass, TEntity>(string jsonString) where TEntity : LayoutEntity where TClass : ILayoutRepository<TEntity>, new() {
        try {
            var jsonOptions = JsonSerializerHelper.Options;
            var jsonObject  = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString, jsonOptions);

            if (jsonObject is null) {
                return new TClass() ?? throw new Exception("Could not create a repository to populate. Fatal");
            }

            // Reconstruct MyObject instance
            var repository     = new TClass() ?? throw new Exception("Could not create a repository to populate. Fatal");
            var properties     = repository.GetType().GetProperties().Where(x => x.DeclaringType == typeof(TClass) && x.CanWrite);
            var collectionName = typeof(TClass).Name;

            foreach (var kvp in jsonObject) {
                if (kvp.Key == collectionName) {
                    var items = (JsonElement)kvp.Value;

                    foreach (var item in items.EnumerateArray()) {
                        var value = item.Deserialize<TEntity>(jsonOptions);
                        if (value is not null) repository.Add(value);
                    }
                } else {
                    var property = repository.GetType().GetProperty(kvp.Key);

                    if (property is not null && property.CanWrite) {
                        try {
                            var value = Convert.ChangeType(kvp.Value.ToString(), property.PropertyType);
                            property.SetValue(repository, value);
                        } catch (InvalidCastException) {
                            // Handle the situation when the conversion cannot be performed
                        }
                    }
                }
            }

            return repository;
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to deserialize configuration data in '{typeof(TClass)}'due to '{ex.Message}'");
        }
    }

    /// <summary>
    /// Serialize the provided instance of a class into a JSON string representation.
    /// The class should implement the ILayoutRepository interface and the LayoutEntity base class.
    /// The serialized JSON string contains the hierarchy of configuration options from this class.
    /// </summary>
    /// <typeparam name="TClass">The type of the class to serialize.</typeparam>
    /// <typeparam name="TEntity">The type of the entities in the class.</typeparam>
    /// <param name="entityClass">The instance of the class to be serialized.</param>
    /// <returns>Returns a JSON string representation of the serialized object.</returns>
    /// <exception cref="ApplicationException">Thrown when serialization fails.</exception>
    public static string SerializeLayout<TClass, TEntity>(TClass entityClass) where TEntity : LayoutEntity where TClass : ILayoutRepository<TEntity> {
        // Write out the Hierarchy of Configuration Options, from this class, to an XML File
        // -----------------------------------------------------------------------------------
        try {
            var jsonOptions = JsonSerializerHelper.Options;
            var jsonObject  = new Dictionary<string, object>();

            // Serialize properties
            var properties = typeof(TClass).GetProperties().Where(x => x.DeclaringType == typeof(TClass) && x.CanWrite);

            foreach (var property in properties) {
                var propertyName                                                                    = property?.Name;
                var propertyValue                                                                   = property?.GetValue(entityClass);
                if (propertyValue is not null && propertyName is not null) jsonObject[propertyName] = propertyValue;
            }

            // Serialize dictionary items, but do not save any Temporary items
            var collectionName = typeof(TClass).Name;
            jsonObject[collectionName] = entityClass.GetAll().Where(x => x.IsTemporary != true);
            var jsonString = JsonSerializer.Serialize(jsonObject, jsonOptions);
            return jsonString;
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to serialize configuration data in '{typeof(TClass)}'due to '{ex.Message}'");
        }
    }
}