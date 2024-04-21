using System.Runtime.CompilerServices;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Manufacturers : EntityCollection<byte, Manufacturer> {

    public Manufacturers() {
        this.Clear();
        BuildManufacturersList();
    }

    public Manufacturer Find(byte identifier) {
        return this[identifier] ?? new Manufacturer("Unknown", 0);
    }

    public Manufacturer Find(string name) {
        return this.FirstOrDefault(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? new Manufacturer("Unknown", 0);
    }

    private void AddNew(Manufacturer manufacturer) {
        if (this.Find(manufacturer.Id).Id != manufacturer.Id) {
            this.Add(manufacturer);
        }
    }

    private void BuildManufacturersList() {
        // Copied from DecoderPro definitions which are from the MNRA Manufacturers List

        AddNew(new Manufacturer("Unknown", 0));
        AddNew(new Manufacturer("A-Train Electronics", 137));
        AddNew(new Manufacturer("AMW", 19));
        AddNew(new Manufacturer("ANE Model Co, Ltd", 45));
        AddNew(new Manufacturer("AXJ Electronics", 110));
        AddNew(new Manufacturer("Advance IC Engineering, Inc.", 17));
        AddNew(new Manufacturer("Aristo-Craft Trains", 34));
        AddNew(new Manufacturer("Arnold - Rivarossi", 173));
        AddNew(new Manufacturer("Atlas", 127));
        AddNew(new Manufacturer("AuroTrains", 170));
        AddNew(new Manufacturer("Auvidel", 76));
        AddNew(new Manufacturer("BLOCKsignalling", 148));
        AddNew(new Manufacturer("BRAWA Modellspielwaren GmbH", 186));
        AddNew(new Manufacturer("Bachmann Trains", 101));
        AddNew(new Manufacturer("Benezan Electronics", 114));
        AddNew(new Manufacturer("Berros", 122));
        AddNew(new Manufacturer("Blue Digital", 225));
        AddNew(new Manufacturer("Bluecher-Electronic", 60));
        AddNew(new Manufacturer("Brelec", 31));
        AddNew(new Manufacturer("Broadway Limited Imports, LLC", 38));
        AddNew(new Manufacturer("CML Systems", 1));
        AddNew(new Manufacturer("CMos Engineering", 130));
        AddNew(new Manufacturer("CT Elektronik", 117));
        AddNew(new Manufacturer("CVP Products", 135));
        AddNew(new Manufacturer("Capecom", 47));
        AddNew(new Manufacturer("Computer Dialysis France", 105));
        AddNew(new Manufacturer("Con-Com GmbH", 204));
        AddNew(new Manufacturer("DCC Supplies, Ltd", 51));
        AddNew(new Manufacturer("DCC Train Automation", 144));
        AddNew(new Manufacturer("DCC-Gaspar-Electronic", 124));
        AddNew(new Manufacturer("DCCconcepts", 36));
        AddNew(new Manufacturer("Dapol Limited", 154));
        AddNew(new Manufacturer("Desktop Station", 140));
        AddNew(new Manufacturer("Dietz Modellbahntechnik", 115));
        AddNew(new Manufacturer("Digi-CZ", 152));
        AddNew(new Manufacturer("Digikeijs", 42));
        AddNew(new Manufacturer("Digirails", 42));
        AddNew(new Manufacturer("Digital Bahn", 64));
        AddNew(new Manufacturer("Digitools Elektronika, Kft", 75));
        AddNew(new Manufacturer("Digitrax", 129));
        AddNew(new Manufacturer("Digsight", 30));
        AddNew(new Manufacturer("Doehler und Haass", 97));
        AddNew(new Manufacturer("E-Modell", 69));
        AddNew(new Manufacturer("ECCO GmbH", 121));
        AddNew(new Manufacturer("Educational Computer, Inc.", 39));
        AddNew(new Manufacturer("Electronic Solutions Ulm GmbH", 151));
        AddNew(new Manufacturer("Electronik and Model Produktion", 35));
        AddNew(new Manufacturer("Electroniscript, inc", 94));
        AddNew(new Manufacturer("Fleischmann", 155));
        AddNew(new Manufacturer("Frateschi Model Trains", 128));
        AddNew(new Manufacturer("Fucik", 158));
        AddNew(new Manufacturer("GFB Designs", 46));
        AddNew(new Manufacturer("Gaugemaster", 65));
        AddNew(new Manufacturer("GooVerModels", 81));
        AddNew(new Manufacturer("HAG Modelleisenbahn AG", 82));
        AddNew(new Manufacturer("HONS Model", 88));
        AddNew(new Manufacturer("Haber and Koenig Electronics GmbH", 111));
        AddNew(new Manufacturer("Harman DCC", 98));
        AddNew(new Manufacturer("Hattons Model Railways", 79));
        AddNew(new Manufacturer("Heljan A/S", 28));
        AddNew(new Manufacturer("Heller Modelbahn", 67));
        AddNew(new Manufacturer("Hornby", 48));
        AddNew(new Manufacturer("Integrated Signal Systems", 102));
        AddNew(new Manufacturer("Intelligent Command Control", 133));
        AddNew(new Manufacturer("JMRI", 18));
        AddNew(new Manufacturer("JSS-Elektronic", 83));
        AddNew(new Manufacturer("Joka Electronic", 49));
        AddNew(new Manufacturer("KAM Industries", 22));
        AddNew(new Manufacturer("KATO Precision Models", 40));
        AddNew(new Manufacturer("KRES GmbH", 58));
        AddNew(new Manufacturer("Kevtronics cc", 93));
        AddNew(new Manufacturer("Kreischer Datatechnik", 21));
        AddNew(new Manufacturer("Krois-Modell", 52));
        AddNew(new Manufacturer("Kuehn Ing.", 157));
        AddNew(new Manufacturer("LDH", 56));
        AddNew(new Manufacturer("LGB", 159));
        AddNew(new Manufacturer("LS Models Sprl", 77));
        AddNew(new Manufacturer("LSdigital", 112));
        AddNew(new Manufacturer("Lahti Associates", 14));
        AddNew(new Manufacturer("LaisDCC", 134));
        AddNew(new Manufacturer("Lenz", 99));
        AddNew(new Manufacturer("MAWE Elektronik", 68));
        AddNew(new Manufacturer("MBTronik - PiN GITmBH", 26));
        AddNew(new Manufacturer("MD Electronics", 160));
        AddNew(new Manufacturer("MERG", 165));
        AddNew(new Manufacturer("MRC", 143));
        AddNew(new Manufacturer("MTB Model", 72));
        AddNew(new Manufacturer("MTH Electric Trains, Inc.", 27));
        AddNew(new Manufacturer("Maison de DCC", 166));
        AddNew(new Manufacturer("Massoth Elektronik, GmbH", 123));
        AddNew(new Manufacturer("Mistral Train Models", 29));
        AddNew(new Manufacturer("MoBaTrain.de", 24));
        AddNew(new Manufacturer("MyLocoSound", 116));
        AddNew(new Manufacturer("MÜT GmbH", 118));
        AddNew(new Manufacturer("Möllehem Gårdsproduktion", 126));
        AddNew(new Manufacturer("N&amp;Q Electronics", 50));
        AddNew(new Manufacturer("NAC Services, Inc", 37));
        AddNew(new Manufacturer("NMRA Reserved", 238));
        AddNew(new Manufacturer("NYRS", 136));
        AddNew(new Manufacturer("Nagasue Controller Design Office", 103));
        AddNew(new Manufacturer("Nagoden", 108));
        AddNew(new Manufacturer("AddNew(new York Byano Limited", 71));
        AddNew(new Manufacturer("Ngineering", 43));
        AddNew(new Manufacturer("Noarail", 63));
        AddNew(new Manufacturer("North Coast Engineering", 11));
        AddNew(new Manufacturer("Nucky", 156));
        AddNew(new Manufacturer("Opherline1", 106));
        AddNew(new Manufacturer("PIKO", 162));
        AddNew(new Manufacturer("PRICOM Design", 96));
        AddNew(new Manufacturer("PSI-Dynatrol", 14));
        AddNew(new Manufacturer("Passmann", 41));
        AddNew(new Manufacturer("Phoenix Sound Systems, Inc.", 107));
        AddNew(new Manufacturer("Pojezdy.EU", 89));
        AddNew(new Manufacturer("PpP Digital", 74));
        AddNew(new Manufacturer("Praecipuus", 33));
        AddNew(new Manufacturer("ProfiLok Modellbahntechnik GmbH", 125));
        AddNew(new Manufacturer("Public-domain and DIY", 13));
        AddNew(new Manufacturer("QElectronics GmbH", 55));
        AddNew(new Manufacturer("QSIndustries", 113));
        AddNew(new Manufacturer("RR-CirKits", 87));
        AddNew(new Manufacturer("Railflyer Model Prototypes, Inc.", 84));
        AddNew(new Manufacturer("Railnet Solutions, LLC", 66));
        AddNew(new Manufacturer("Rails Europ Express", 146));
        AddNew(new Manufacturer("Railstars Limited", 91));
        AddNew(new Manufacturer("RamFixx Technologies (Wangrow)", 15));
        AddNew(new Manufacturer("Rampino Elektronik", 57));
        AddNew(new Manufacturer("Rautenhaus Digital Vertrieb", 53));
        AddNew(new Manufacturer("RealRail Effects", 139));
        AddNew(new Manufacturer("Regal Way Co. Ltd", 32));
        AddNew(new Manufacturer("Rock Junction Controls", 149));
        AddNew(new Manufacturer("Roco Modelspielwaren", 161));
        AddNew(new Manufacturer("Rocrail", 70));
        AddNew(new Manufacturer("S Helper Service", 23));
        AddNew(new Manufacturer("SLOMO Railroad Models", 142));
        AddNew(new Manufacturer("SPROG DCC", 44));
        AddNew(new Manufacturer("Sanda Kan Industrial", 95));
        AddNew(new Manufacturer("Shourt Line", 90));
        AddNew(new Manufacturer("Signaling Solution", 119));
        AddNew(new Manufacturer("Silicon Railway", 33));
        AddNew(new Manufacturer("SoundTraxx (Throttle-Up)", 141));
        AddNew(new Manufacturer("Spectrum Engineering", 80));
        AddNew(new Manufacturer("T4T - Technology for Trains GmbH", 20));
        AddNew(new Manufacturer("TCH Technology", 54));
        AddNew(new Manufacturer("Tam Valley Depot", 59));
        AddNew(new Manufacturer("Tams Elektronik GmbH", 62));
        AddNew(new Manufacturer("Tawcrafts", 92));
        AddNew(new Manufacturer("Team Digital, LLC", 25));
        AddNew(new Manufacturer("Tehnologistic (train-O-matic)", 78));
        AddNew(new Manufacturer("The Electric Railroad Company", 73));
        AddNew(new Manufacturer("Throttle-Up (SoundTraxx)", 141));
        AddNew(new Manufacturer("Train Control Systems", 153));
        AddNew(new Manufacturer("Train ID Systems", 138));
        AddNew(new Manufacturer("Train Technology", 2));
        AddNew(new Manufacturer("TrainModules", 61));
        AddNew(new Manufacturer("TrainTech", 104));
        AddNew(new Manufacturer("Trenes Digitales", 100));
        AddNew(new Manufacturer("Trix Modelleisenbahn", 131));
        AddNew(new Manufacturer("Uhlenbrock Elektronik", 85));
        AddNew(new Manufacturer("Umelec Ing Buero", 147));
        AddNew(new Manufacturer("Viessmann Modellspielwaren GmbH", 109));
        AddNew(new Manufacturer("W.S. Ataras Engineering", 119));
        AddNew(new Manufacturer("WP Railshops", 163));
        AddNew(new Manufacturer("Wangrow", 12));
        AddNew(new Manufacturer("Wekomm Engineering, GmbH", 86));
        AddNew(new Manufacturer("Wm. K. Walthers, Inc", 150));
        AddNew(new Manufacturer("ZTC", 132));
        AddNew(new Manufacturer("Zimo", 145));
        AddNew(new Manufacturer("csikos-muhely", 120));
        AddNew(new Manufacturer("drM", 164));
    }
}