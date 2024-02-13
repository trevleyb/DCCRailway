using System.Xml.Serialization;

namespace DCCRailway.System.Configuration.Conversion.JMRI;

[XmlRoot(ElementName = "decoder")]
public class Decoder {
    [XmlAttribute(AttributeName = "model")]
    public string Model { get; set; }

    [XmlAttribute(AttributeName = "family")]
    public string Family { get; set; }

    [XmlAttribute(AttributeName = "comment")]
    public string Comment { get; set; }
}

[XmlRoot(ElementName = "dcclocoaddress")]
public class Dcclocoaddress {
    [XmlAttribute(AttributeName = "number")]
    public string Number { get; set; }

    [XmlAttribute(AttributeName = "longaddress")]
    public string Longaddress { get; set; }
}

[XmlRoot(ElementName = "locoaddress")]
public class Locoaddress {
    [XmlElement(ElementName = "dcclocoaddress")]
    public Dcclocoaddress Dcclocoaddress { get; set; }

    [XmlElement(ElementName = "number")]
    public string Number { get; set; }

    [XmlElement(ElementName = "protocol")]
    public string Protocol { get; set; }
}

[XmlRoot(ElementName = "functionlabel")]
public class Functionlabel {
    [XmlAttribute(AttributeName = "num")]
    public string Num { get; set; }

    [XmlAttribute(AttributeName = "lockable")]
    public string Lockable { get; set; }

    [XmlAttribute(AttributeName = "functionImage")]
    public string FunctionImage { get; set; }

    [XmlAttribute(AttributeName = "functionImageSelected")]
    public string FunctionImageSelected { get; set; }

    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "functionlabels")]
public class Functionlabels {
    [XmlElement(ElementName = "functionlabel")]
    public List<Functionlabel> Functionlabel { get; set; }
}

[XmlRoot(ElementName = "soundlabel")]
public class Soundlabel {
    [XmlAttribute(AttributeName = "num")]
    public string Num { get; set; }

    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "soundlabels")]
public class Soundlabels {
    [XmlElement(ElementName = "soundlabel")]
    public List<Soundlabel> Soundlabel { get; set; }
}

[XmlRoot(ElementName = "keyvaluepair")]
public class Keyvaluepair {
    [XmlElement(ElementName = "key")]
    public string Key { get; set; }

    [XmlElement(ElementName = "value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "attributepairs")]
public class Attributepairs {
    [XmlElement(ElementName = "keyvaluepair")]
    public List<Keyvaluepair> Keyvaluepair { get; set; }
}

[XmlRoot(ElementName = "locomotive")]
public class Locomotive {
    [XmlElement(ElementName = "dateUpdated")]
    public string DateUpdated { get; set; }

    [XmlElement(ElementName = "decoder")]
    public Decoder Decoder { get; set; }

    [XmlElement(ElementName = "locoaddress")]
    public Locoaddress Locoaddress { get; set; }

    [XmlElement(ElementName = "functionlabels")]
    public Functionlabels Functionlabels { get; set; }

    [XmlElement(ElementName = "soundlabels")]
    public Soundlabels Soundlabels { get; set; }

    [XmlElement(ElementName = "attributepairs")]
    public Attributepairs Attributepairs { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "fileName")]
    public string FileName { get; set; }

    [XmlAttribute(AttributeName = "roadNumber")]
    public string RoadNumber { get; set; }

    [XmlAttribute(AttributeName = "roadName")]
    public string RoadName { get; set; }

    [XmlAttribute(AttributeName = "mfg")]
    public string Mfg { get; set; }

    [XmlAttribute(AttributeName = "owner")]
    public string Owner { get; set; }

    [XmlAttribute(AttributeName = "model")]
    public string Model { get; set; }

    [XmlAttribute(AttributeName = "dccAddress")]
    public string DccAddress { get; set; }

    [XmlAttribute(AttributeName = "comment")]
    public string Comment { get; set; }

    [XmlAttribute(AttributeName = "maxSpeed")]
    public string MaxSpeed { get; set; }

    [XmlAttribute(AttributeName = "imageFilePath")]
    public string ImageFilePath { get; set; }

    [XmlAttribute(AttributeName = "iconFilePath")]
    public string IconFilePath { get; set; }

    [XmlAttribute(AttributeName = "URL")]
    public string URL { get; set; }

    [XmlAttribute(AttributeName = "IsShuntingOn")]
    public string IsShuntingOn { get; set; }
}

[XmlRoot(ElementName = "roster")]
public class Roster {
    [XmlElement(ElementName = "locomotive")]
    public List<Locomotive> Locomotive { get; set; }
}

[XmlRoot(ElementName = "rosterGroup")]
public class RosterGroup {
    [XmlElement(ElementName = "group")]
    public List<string> Group { get; set; }
}

[XmlRoot(ElementName = "roster-config")]
public class Rosterconfig : ConfigSerializer<Rosterconfig> {
    [XmlElement(ElementName = "roster")]
    public Roster Roster { get; set; }

    [XmlElement(ElementName = "rosterGroup")]
    public RosterGroup RosterGroup { get; set; }

    [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Xsi { get; set; }

    [XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public string NoNamespaceSchemaLocation { get; set; }
}