﻿using System.Xml.Serialization;

namespace DCCRailway.System.Configuration;

[XmlRoot(ElementName = "Controller")]
public class System {
    public System() => Parameters = new Parameters();

    public System(string name) => Name = name;

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Adapter")]
    public Adapter Adapter { get; set; }

    [XmlArray(ElementName = "Parameters")]
    public Parameters Parameters { get; set; }
}