using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DCCRailway.Core.Config;

public class ConfigSerializer<T> {
    #region Load and Save Functions
    /// <summary>
    ///     Load an instance of class T from a provided filename and throw an exception if the
    ///     file name does not exist.
    /// </summary>
    /// <param name="name">The name of the file to load</param>
    /// <returns>An instance of an XML class </returns>
    /// <exception cref="ApplicationException">If it is unable to load the file</exception>
    public static T? Load(string name) {
        try {
            if (!File.Exists(name)) throw new FileNotFoundException($"Unable to access file '{name}'");
            XmlSerializer xmlSerializer = new(typeof(T));

            if (xmlSerializer != null) {
                using TextReader reader = new StreamReader(name);

                if (xmlSerializer.Deserialize(reader!) is not T xmlFile) throw new ApplicationException($"Unable to load the configuration file '{name}' as the deserializer did not return correctly.");

                return xmlFile;
            }

            throw new ApplicationException($"Unable to load the configuration file '{name}' due to issues with the XML serializer.");
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to load the configuration file '{name}' due to '{ex.Message}'", ex);
        }
    }

    /// <summary>
    ///     Provide a file name for the configuration and save to that file
    /// </summary>
    /// <param name="name">The name of the file to save to</param>
    public void Save(T xmlObject, string name) {
        if (string.IsNullOrEmpty(name)) throw new ApplicationException("You must specify a name for the Configuration File.");

        // Write out the Hierarchy of Configuration Options, from this class, to an XML File
        // -----------------------------------------------------------------------------------
        try {
            var xmlWriterSettings = new XmlWriterSettings { Indent = true };
            xmlWriterSettings.OmitXmlDeclaration = true;

            XmlSerializer xmlSerializer = new(typeof(T));
            using var xmlWriter = XmlWriter.Create(name, xmlWriterSettings);
            xmlSerializer.Serialize(xmlWriter, xmlObject);
            xmlWriter.Close();
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to save configuration data to '{name}' due to '{ex.Message}'");
        }
    }
    #endregion
}