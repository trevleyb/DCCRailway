﻿using System.Xml.Serialization;

namespace DCCRailway.System.Config;

public class Adapter {
    public Adapter() => Parameters = new Parameters();

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlArray(ElementName = "Parameters")]
    public Parameters Parameters { get; set; }
}