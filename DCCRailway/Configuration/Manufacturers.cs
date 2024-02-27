﻿using DCCRailway.Configuration.Entities.Base;

namespace DCCRailway.Configuration;
public class Manufacturers : ConfigCollectionBase<Manufacturer> {
    public Manufacturers() => BuildDefaults();

    // <decoder model ="LokSound Select direct / micro" family="ESU LokSound Select" comment=""/>

    public Manufacturer? FindByManufacturer(string manufacturer) {
        return this.Where(name => name.Name.Equals(manufacturer)).FirstOrDefault();
    }

    public Manufacturer? FindByIdentifier(byte identifier) {
        return this.Where(manufacturer => manufacturer.Identifier.Equals(identifier)).FirstOrDefault();
    }

    public void BuildDefaults() {
        // Copied from DecoderPro definitions which are from the MNRA Manufacturers List
        Add(new Manufacturer(name: "Unknown", identifier: 0));
        Add(new Manufacturer(name: "A-Train Electronics", identifier: 137));
        Add(new Manufacturer(name: "AMW", identifier: 19));
        Add(new Manufacturer(name: "ANE Model Co, Ltd", identifier: 45));
        Add(new Manufacturer(name: "AXJ Electronics", identifier: 110));
        Add(new Manufacturer(name: "Advance IC Engineering, Inc.", identifier: 17));
        Add(new Manufacturer(name: "Aristo-Craft Trains", identifier: 34));
        Add(new Manufacturer(name: "Arnold - Rivarossi", identifier: 173));
        Add(new Manufacturer(name: "Atlas", identifier: 127));
        Add(new Manufacturer(name: "AuroTrains", identifier: 170));
        Add(new Manufacturer(name: "Auvidel", identifier: 76));
        Add(new Manufacturer(name: "BLOCKsignalling", identifier: 148));
        Add(new Manufacturer(name: "BRAWA Modellspielwaren GmbH", identifier: 186));
        Add(new Manufacturer(name: "Bachmann Trains", identifier: 101));
        Add(new Manufacturer(name: "Benezan Electronics", identifier: 114));
        Add(new Manufacturer(name: "Berros", identifier: 122));
        Add(new Manufacturer(name: "Blue Digital", identifier: 225));
        Add(new Manufacturer(name: "Bluecher-Electronic", identifier: 60));
        Add(new Manufacturer(name: "Brelec", identifier: 31));
        Add(new Manufacturer(name: "Broadway Limited Imports, LLC", identifier: 38));
        Add(new Manufacturer(name: "CML Systems", identifier: 1));
        Add(new Manufacturer(name: "CMos Engineering", identifier: 130));
        Add(new Manufacturer(name: "CT Elektronik", identifier: 117));
        Add(new Manufacturer(name: "CVP Products", identifier: 135));
        Add(new Manufacturer(name: "Capecom", identifier: 47));
        Add(new Manufacturer(name: "Computer Dialysis France", identifier: 105));
        Add(new Manufacturer(name: "Con-Com GmbH", identifier: 204));
        Add(new Manufacturer(name: "DCC Supplies, Ltd", identifier: 51));
        Add(new Manufacturer(name: "DCC Train Automation", identifier: 144));
        Add(new Manufacturer(name: "DCC-Gaspar-Electronic", identifier: 124));
        Add(new Manufacturer(name: "DCCconcepts", identifier: 36));
        Add(new Manufacturer(name: "Dapol Limited", identifier: 154));
        Add(new Manufacturer(name: "Desktop Station", identifier: 140));
        Add(new Manufacturer(name: "Dietz Modellbahntechnik", identifier: 115));
        Add(new Manufacturer(name: "Digi-CZ", identifier: 152));
        Add(new Manufacturer(name: "Digikeijs", identifier: 42));
        Add(new Manufacturer(name: "Digirails", identifier: 42));
        Add(new Manufacturer(name: "Digital Bahn", identifier: 64));
        Add(new Manufacturer(name: "Digitools Elektronika, Kft", identifier: 75));
        Add(new Manufacturer(name: "Digitrax", identifier: 129));
        Add(new Manufacturer(name: "Digsight", identifier: 30));
        Add(new Manufacturer(name: "Doehler und Haass", identifier: 97));
        Add(new Manufacturer(name: "E-Modell", identifier: 69));
        Add(new Manufacturer(name: "ECCO GmbH", identifier: 121));
        Add(new Manufacturer(name: "Educational Computer, Inc.", identifier: 39));
        Add(new Manufacturer(name: "Electronic Solutions Ulm GmbH", identifier: 151));
        Add(new Manufacturer(name: "Electronik and Model Produktion", identifier: 35));
        Add(new Manufacturer(name: "Electroniscript, inc", identifier: 94));
        Add(new Manufacturer(name: "Fleischmann", identifier: 155));
        Add(new Manufacturer(name: "Frateschi Model Trains", identifier: 128));
        Add(new Manufacturer(name: "Fucik", identifier: 158));
        Add(new Manufacturer(name: "GFB Designs", identifier: 46));
        Add(new Manufacturer(name: "Gaugemaster", identifier: 65));
        Add(new Manufacturer(name: "GooVerModels", identifier: 81));
        Add(new Manufacturer(name: "HAG Modelleisenbahn AG", identifier: 82));
        Add(new Manufacturer(name: "HONS Model", identifier: 88));
        Add(new Manufacturer(name: "Haber and Koenig Electronics GmbH", identifier: 111));
        Add(new Manufacturer(name: "Harman DCC", identifier: 98));
        Add(new Manufacturer(name: "Hattons Model Railways", identifier: 79));
        Add(new Manufacturer(name: "Heljan A/S", identifier: 28));
        Add(new Manufacturer(name: "Heller Modelbahn", identifier: 67));
        Add(new Manufacturer(name: "Hornby", identifier: 48));
        Add(new Manufacturer(name: "Integrated Signal Systems", identifier: 102));
        Add(new Manufacturer(name: "Intelligent Command Control", identifier: 133));
        Add(new Manufacturer(name: "JMRI", identifier: 18));
        Add(new Manufacturer(name: "JSS-Elektronic", identifier: 83));
        Add(new Manufacturer(name: "Joka Electronic", identifier: 49));
        Add(new Manufacturer(name: "KAM Industries", identifier: 22));
        Add(new Manufacturer(name: "KATO Precision Models", identifier: 40));
        Add(new Manufacturer(name: "KRES GmbH", identifier: 58));
        Add(new Manufacturer(name: "Kevtronics cc", identifier: 93));
        Add(new Manufacturer(name: "Kreischer Datatechnik", identifier: 21));
        Add(new Manufacturer(name: "Krois-Modell", identifier: 52));
        Add(new Manufacturer(name: "Kuehn Ing.", identifier: 157));
        Add(new Manufacturer(name: "LDH", identifier: 56));
        Add(new Manufacturer(name: "LGB", identifier: 159));
        Add(new Manufacturer(name: "LS Models Sprl", identifier: 77));
        Add(new Manufacturer(name: "LSdigital", identifier: 112));
        Add(new Manufacturer(name: "Lahti Associates", identifier: 14));
        Add(new Manufacturer(name: "LaisDCC", identifier: 134));
        Add(new Manufacturer(name: "Lenz", identifier: 99));
        Add(new Manufacturer(name: "MAWE Elektronik", identifier: 68));
        Add(new Manufacturer(name: "MBTronik - PiN GITmBH", identifier: 26));
        Add(new Manufacturer(name: "MD Electronics", identifier: 160));
        Add(new Manufacturer(name: "MERG", identifier: 165));
        Add(new Manufacturer(name: "MRC", identifier: 143));
        Add(new Manufacturer(name: "MTB Model", identifier: 72));
        Add(new Manufacturer(name: "MTH Electric Trains, Inc.", identifier: 27));
        Add(new Manufacturer(name: "Maison de DCC", identifier: 166));
        Add(new Manufacturer(name: "Massoth Elektronik, GmbH", identifier: 123));
        Add(new Manufacturer(name: "Mistral Train Models", identifier: 29));
        Add(new Manufacturer(name: "MoBaTrain.de", identifier: 24));
        Add(new Manufacturer(name: "MyLocoSound", identifier: 116));
        Add(new Manufacturer(name: "MÜT GmbH", identifier: 118));
        Add(new Manufacturer(name: "Möllehem Gårdsproduktion", identifier: 126));
        Add(new Manufacturer(name: "N&amp;Q Electronics", identifier: 50));
        Add(new Manufacturer(name: "NAC Services, Inc", identifier: 37));
        Add(new Manufacturer(name: "NMRA Reserved", identifier: 238));
        Add(new Manufacturer(name: "NYRS", identifier: 136));
        Add(new Manufacturer(name: "Nagasue Controller Design Office", identifier: 103));
        Add(new Manufacturer(name: "Nagoden", identifier: 108));
        Add(new Manufacturer(name: "New York Byano Limited", identifier: 71));
        Add(new Manufacturer(name: "Ngineering", identifier: 43));
        Add(new Manufacturer(name: "Noarail", identifier: 63));
        Add(new Manufacturer(name: "North Coast Engineering", identifier: 11));
        Add(new Manufacturer(name: "Nucky", identifier: 156));
        Add(new Manufacturer(name: "Opherline1", identifier: 106));
        Add(new Manufacturer(name: "PIKO", identifier: 162));
        Add(new Manufacturer(name: "PRICOM Design", identifier: 96));
        Add(new Manufacturer(name: "PSI-Dynatrol", identifier: 14));
        Add(new Manufacturer(name: "Passmann", identifier: 41));
        Add(new Manufacturer(name: "Phoenix Sound Systems, Inc.", identifier: 107));
        Add(new Manufacturer(name: "Pojezdy.EU", identifier: 89));
        Add(new Manufacturer(name: "PpP Digital", identifier: 74));
        Add(new Manufacturer(name: "Praecipuus", identifier: 33));
        Add(new Manufacturer(name: "ProfiLok Modellbahntechnik GmbH", identifier: 125));
        Add(new Manufacturer(name: "Public-domain and DIY", identifier: 13));
        Add(new Manufacturer(name: "QElectronics GmbH", identifier: 55));
        Add(new Manufacturer(name: "QSIndustries", identifier: 113));
        Add(new Manufacturer(name: "RR-CirKits", identifier: 87));
        Add(new Manufacturer(name: "Railflyer Model Prototypes, Inc.", identifier: 84));
        Add(new Manufacturer(name: "Railnet Solutions, LLC", identifier: 66));
        Add(new Manufacturer(name: "Rails Europ Express", identifier: 146));
        Add(new Manufacturer(name: "Railstars Limited", identifier: 91));
        Add(new Manufacturer(name: "RamFixx Technologies (Wangrow)", identifier: 15));
        Add(new Manufacturer(name: "Rampino Elektronik", identifier: 57));
        Add(new Manufacturer(name: "Rautenhaus Digital Vertrieb", identifier: 53));
        Add(new Manufacturer(name: "RealRail Effects", identifier: 139));
        Add(new Manufacturer(name: "Regal Way Co. Ltd", identifier: 32));
        Add(new Manufacturer(name: "Rock Junction Controls", identifier: 149));
        Add(new Manufacturer(name: "Roco Modelspielwaren", identifier: 161));
        Add(new Manufacturer(name: "Rocrail", identifier: 70));
        Add(new Manufacturer(name: "S Helper Service", identifier: 23));
        Add(new Manufacturer(name: "SLOMO Railroad Models", identifier: 142));
        Add(new Manufacturer(name: "SPROG DCC", identifier: 44));
        Add(new Manufacturer(name: "Sanda Kan Industrial", identifier: 95));
        Add(new Manufacturer(name: "Shourt Line", identifier: 90));
        Add(new Manufacturer(name: "Signaling Solution", identifier: 119));
        Add(new Manufacturer(name: "Silicon Railway", identifier: 33));
        Add(new Manufacturer(name: "SoundTraxx (Throttle-Up)", identifier: 141));
        Add(new Manufacturer(name: "Spectrum Engineering", identifier: 80));
        Add(new Manufacturer(name: "T4T - Technology for Trains GmbH", identifier: 20));
        Add(new Manufacturer(name: "TCH Technology", identifier: 54));
        Add(new Manufacturer(name: "Tam Valley Depot", identifier: 59));
        Add(new Manufacturer(name: "Tams Elektronik GmbH", identifier: 62));
        Add(new Manufacturer(name: "Tawcrafts", identifier: 92));
        Add(new Manufacturer(name: "Team Digital, LLC", identifier: 25));
        Add(new Manufacturer(name: "Tehnologistic (train-O-matic)", identifier: 78));
        Add(new Manufacturer(name: "The Electric Railroad Company", identifier: 73));
        Add(new Manufacturer(name: "Throttle-Up (SoundTraxx)", identifier: 141));
        Add(new Manufacturer(name: "Train Control Systems", identifier: 153));
        Add(new Manufacturer(name: "Train ID Systems", identifier: 138));
        Add(new Manufacturer(name: "Train Technology", identifier: 2));
        Add(new Manufacturer(name: "TrainModules", identifier: 61));
        Add(new Manufacturer(name: "TrainTech", identifier: 104));
        Add(new Manufacturer(name: "Trenes Digitales", identifier: 100));
        Add(new Manufacturer(name: "Trix Modelleisenbahn", identifier: 131));
        Add(new Manufacturer(name: "Uhlenbrock Elektronik", identifier: 85));
        Add(new Manufacturer(name: "Umelec Ing Buero", identifier: 147));
        Add(new Manufacturer(name: "Viessmann Modellspielwaren GmbH", identifier: 109));
        Add(new Manufacturer(name: "W.S. Ataras Engineering", identifier: 119));
        Add(new Manufacturer(name: "WP Railshops", identifier: 163));
        Add(new Manufacturer(name: "Wangrow", identifier: 12));
        Add(new Manufacturer(name: "Wekomm Engineering, GmbH", identifier: 86));
        Add(new Manufacturer(name: "Wm. K. Walthers, Inc", identifier: 150));
        Add(new Manufacturer(name: "ZTC", identifier: 132));
        Add(new Manufacturer(name: "Zimo", identifier: 145));
        Add(new Manufacturer(name: "csikos-muhely", identifier: 120));
        Add(new Manufacturer(name: "drM", identifier: 164));
    }
}