﻿using System.Xml.Serialization;
using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Configuration.Entities;

[XmlRoot(ElementName = "Accessory")]
public class Accessory : IAccessory{
    public Accessory() {
        Parameters = new Parameters();
        Decoder    = new Decoder { AddressType = DCCAddressType.Accessory };
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "Description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "Decoder")]
    public Decoder Decoder { get; set; }

    [XmlArray(ElementName = "Parameters")]
    public Parameters Parameters { get; set; }
}