using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using DccRailway.API.Entities;

namespace DccRailway.API.Repositories;

public class SerializedRepository<TEntity, TID> : IRepository<TEntity, TID> where TEntity : IEntity<TID> where TID : IComparable {
    private readonly List<TEntity> _entities;

    private string _filename;

    public SerializedRepository(string filename) {
        _filename = filename;
        _entities = Load(_filename) ?? new List<TEntity>();
    }

    public SerializedRepository() {
        _filename = string.Empty;
        _entities = Load(_filename) ?? new List<TEntity>();
    }

    public TEntity? GetById(TID id) {
        return _entities.FirstOrDefault(item => item.Id.Equals(id));
    }

    public IEnumerable<TEntity> GetAll() {
        return _entities;
    }

    public TID Add(TEntity obj) {
        if (obj == null) throw new NullReferenceException("Cannot add a null object.");
        obj.Id = obj.GenerateID();
        _entities.Add(obj);

        return obj.Id;
    }

    public void Update(TEntity obj) {
        var index = _entities.FindIndex(x => x.Id.Equals(obj.Id));

        if (index == -1) throw new InvalidDataException("Specified object does not exist. Needs to be added first.");
        _entities[index] = obj;
    }

    public void Delete(TEntity obj) {
        var index = _entities.FindIndex(x => x.Id.Equals(obj.Id));

        if (index == -1) throw new InvalidDataException("Specified object does not exist. No need to delete it.");
        _entities.RemoveAt(index);
    }

    public void Delete(TID id) {
        var index = _entities.FindIndex(x => x.Id.Equals(id));

        if (index == -1) throw new InvalidDataException("Specified object does not exist. No need to delete it.");
        _entities.RemoveAt(index);
    }

    public void Save() {
        Save(_entities, _filename);
    }

    public void Save(string filename) {
        _filename = filename;
        Save(_entities, _filename);
    }

    #region Load and Save Functions
    /// <summary>
    ///     Load an instance of class T from a provided filename and throw an exception if the
    ///     file name does not exist.
    /// </summary>
    /// <param name="name">The name of the file to load</param>
    /// <returns>An instance of an XML class </returns>
    /// <exception cref="ApplicationException">If it is unable to load the file</exception>
    private List<TEntity>? Load(string name) {
        try {
            if (!File.Exists(name)) return null;

            var elementName = typeof(TEntity).Name;
            XmlSerializer xmlSerializer = new(typeof(XmlEntity), new XmlRootAttribute(elementName));

            using (TextReader reader = new StreamReader(name)) {
                if (xmlSerializer.Deserialize(reader!) is not XmlEntity xmlFile) throw new ApplicationException($"Unable to load the configuration file '{name}' as the deserializer did not return correctly.");

                return xmlFile.Item;
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
    private void Save(List<TEntity> entityList, string name) {
        if (string.IsNullOrEmpty(name)) throw new ApplicationException("You must specify a name for the Configuration File.");

        // Write out the Hierarchy of Configuration Options, from this class, to an XML File
        // -----------------------------------------------------------------------------------
        try {
            var xmlWriterSettings = new XmlWriterSettings { Indent = true };
            xmlWriterSettings.OmitXmlDeclaration = true;

            var elementName = typeof(TEntity).Name;
            //XmlAttributes xmlAttributes = new XmlAttributes();
            //xmlAttributes.XmlIgnore = true;
            //xmlAttributes.XmlRoot = new XmlRootAttribute(elementName);
            //xmlAttributes.Xmlns = false;

            XmlSerializer xmlSerializer = new(typeof(XmlEntity), new XmlRootAttribute(elementName));

            using (var xmlWriter = XmlWriter.Create(name, xmlWriterSettings)) {
                xmlSerializer.Serialize(xmlWriter, new XmlEntity { EntityList = entityList });
                xmlWriter.Close();
            }
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to save configuration data to '{name}' due to '{ex.Message}'");
        }
    }


    [XmlRoot]
    public class XmlEntity {
        [XmlIgnore] public IEnumerable<TEntity> EntityList { get; set; }

        [XmlElement]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<TEntity> Item {
            get => EntityList.ToList();
            set => EntityList = value;
        }
    }
    #endregion
}