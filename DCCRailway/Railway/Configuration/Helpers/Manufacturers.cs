namespace DCCRailway.Railway.Configuration.Helpers;

[Serializable]
public class Manufacturers : Dictionary<byte, Manufacturer>  {

    public Manufacturers() {
        Clear();
        BuildManufacturersList();
    }

    public new Manufacturer? this[byte id] => Find(id);

    public Manufacturer? Find(byte identifier) {
        return this[identifier] ?? new Manufacturer(0,"Unknown");
    }

    public Manufacturer? Find(string name) {
        return this.Values.FirstOrDefault(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? new Manufacturer(0,"Unknown");
    }

    public Manufacturer? Find(Func<Manufacturer, bool> predicate) {
        return Values.Where(predicate).FirstOrDefault();
    }

    public IEnumerable<Manufacturer>? FindAll(Func<Manufacturer, bool> predicate) {
        return Values.Where(predicate);
    }

    private void AddManufacturer(string name, byte id) {
        if (!this.ContainsKey(id)) Add(id,new Manufacturer(id,name));
    }

    private void BuildManufacturersList() {
        // Copied from DecoderPro definitions which are from the MNRA Manufacturers List

       AddManufacturer("Unknown", 0);
       AddManufacturer("A-Train Electronics", 137);
       AddManufacturer("AMW", 19);
       AddManufacturer("ANE Model Co, Ltd", 45);
       AddManufacturer("AXJ Electronics", 110);
       AddManufacturer("Advance IC Engineering, Inc.", 17);
       AddManufacturer("Aristo-Craft Trains", 34);
       AddManufacturer("Arnold - Rivarossi", 173);
       AddManufacturer("Atlas", 127);
       AddManufacturer("AuroTrains", 170);
       AddManufacturer("Auvidel", 76);
       AddManufacturer("BLOCKsignalling", 148);
       AddManufacturer("BRAWA Modellspielwaren GmbH", 186);
       AddManufacturer("Bachmann Trains", 101);
       AddManufacturer("Benezan Electronics", 114);
       AddManufacturer("Berros", 122);
       AddManufacturer("Blue Digital", 225);
       AddManufacturer("Bluecher-Electronic", 60);
       AddManufacturer("Brelec", 31);
       AddManufacturer("Broadway Limited Imports, LLC", 38);
       AddManufacturer("CML Systems", 1);
       AddManufacturer("CMos Engineering", 130);
       AddManufacturer("CT Elektronik", 117);
       AddManufacturer("CVP Products", 135);
       AddManufacturer("Capecom", 47);
       AddManufacturer("Computer Dialysis France", 105);
       AddManufacturer("Con-Com GmbH", 204);
       AddManufacturer("DCC Supplies, Ltd", 51);
       AddManufacturer("DCC Train Automation", 144);
       AddManufacturer("DCC-Gaspar-Electronic", 124);
       AddManufacturer("DCCconcepts", 36);
       AddManufacturer("Dapol Limited", 154);
       AddManufacturer("Desktop Station", 140);
       AddManufacturer("Dietz Modellbahntechnik", 115);
       AddManufacturer("Digi-CZ", 152);
       AddManufacturer("Digikeijs", 42);
       AddManufacturer("Digirails", 42);
       AddManufacturer("Digital Bahn", 64);
       AddManufacturer("Digitools Elektronika, Kft", 75);
       AddManufacturer("Digitrax", 129);
       AddManufacturer("Digsight", 30);
       AddManufacturer("Doehler und Haass", 97);
       AddManufacturer("E-Modell", 69);
       AddManufacturer("ECCO GmbH", 121);
       AddManufacturer("Educational Computer, Inc.", 39);
       AddManufacturer("Electronic Solutions Ulm GmbH", 151);
       AddManufacturer("Electronik and Model Produktion", 35);
       AddManufacturer("Electroniscript, inc", 94);
       AddManufacturer("Fleischmann", 155);
       AddManufacturer("Frateschi Model Trains", 128);
       AddManufacturer("Fucik", 158);
       AddManufacturer("GFB Designs", 46);
       AddManufacturer("Gaugemaster", 65);
       AddManufacturer("GooVerModels", 81);
       AddManufacturer("HAG Modelleisenbahn AG", 82);
       AddManufacturer("HONS Model", 88);
       AddManufacturer("Haber and Koenig Electronics GmbH", 111);
       AddManufacturer("Harman DCC", 98);
       AddManufacturer("Hattons Model Railways", 79);
       AddManufacturer("Heljan A/S", 28);
       AddManufacturer("Heller Modelbahn", 67);
       AddManufacturer("Hornby", 48);
       AddManufacturer("Integrated Signal Systems", 102);
       AddManufacturer("Intelligent Command Control", 133);
       AddManufacturer("JMRI", 18);
       AddManufacturer("JSS-Elektronic", 83);
       AddManufacturer("Joka Electronic", 49);
       AddManufacturer("KAM Industries", 22);
       AddManufacturer("KATO Precision Models", 40);
       AddManufacturer("KRES GmbH", 58);
       AddManufacturer("Kevtronics cc", 93);
       AddManufacturer("Kreischer Datatechnik", 21);
       AddManufacturer("Krois-Modell", 52);
       AddManufacturer("Kuehn Ing.", 157);
       AddManufacturer("LDH", 56);
       AddManufacturer("LGB", 159);
       AddManufacturer("LS Models Sprl", 77);
       AddManufacturer("LSdigital", 112);
       AddManufacturer("Lahti Associates", 14);
       AddManufacturer("LaisDCC", 134);
       AddManufacturer("Lenz", 99);
       AddManufacturer("MAWE Elektronik", 68);
       AddManufacturer("MBTronik - PiN GITmBH", 26);
       AddManufacturer("MD Electronics", 160);
       AddManufacturer("MERG", 165);
       AddManufacturer("MRC", 143);
       AddManufacturer("MTB Model", 72);
       AddManufacturer("MTH Electric Trains, Inc.", 27);
       AddManufacturer("Maison de DCC", 166);
       AddManufacturer("Massoth Elektronik, GmbH", 123);
       AddManufacturer("Mistral Train Models", 29);
       AddManufacturer("MoBaTrain.de", 24);
       AddManufacturer("MyLocoSound", 116);
       AddManufacturer("MÜT GmbH", 118);
       AddManufacturer("Möllehem Gårdsproduktion", 126);
       AddManufacturer("N&amp;Q Electronics", 50);
       AddManufacturer("NAC Services, Inc", 37);
       AddManufacturer("NMRA Reserved", 238);
       AddManufacturer("NYRS", 136);
       AddManufacturer("Nagasue CommandStation Design Office", 103);
       AddManufacturer("Nagoden", 108);
       AddManufacturer("AddNew(new York Byano Limited", 71);
       AddManufacturer("Ngineering", 43);
       AddManufacturer("Noarail", 63);
       AddManufacturer("North Coast Engineering", 11);
       AddManufacturer("Nucky", 156);
       AddManufacturer("Opherline1", 106);
       AddManufacturer("PIKO", 162);
       AddManufacturer("PRICOM Design", 96);
       AddManufacturer("PSI-Dynatrol", 14);
       AddManufacturer("Passmann", 41);
       AddManufacturer("Phoenix Sound Systems, Inc.", 107);
       AddManufacturer("Pojezdy.EU", 89);
       AddManufacturer("PpP Digital", 74);
       AddManufacturer("Praecipuus", 33);
       AddManufacturer("ProfiLok Modellbahntechnik GmbH", 125);
       AddManufacturer("Public-domain and DIY", 13);
       AddManufacturer("QElectronics GmbH", 55);
       AddManufacturer("QSIndustries", 113);
       AddManufacturer("RR-CirKits", 87);
       AddManufacturer("Railflyer Model Prototypes, Inc.", 84);
       AddManufacturer("Railnet Solutions, LLC", 66);
       AddManufacturer("Rails Europ Express", 146);
       AddManufacturer("Railstars Limited", 91);
       AddManufacturer("RamFixx Technologies (Wangrow)", 15);
       AddManufacturer("Rampino Elektronik", 57);
       AddManufacturer("Rautenhaus Digital Vertrieb", 53);
       AddManufacturer("RealRail Effects", 139);
       AddManufacturer("Regal Way Co. Ltd", 32);
       AddManufacturer("Rock Junction Controls", 149);
       AddManufacturer("Roco Modelspielwaren", 161);
       AddManufacturer("Rocrail", 70);
       AddManufacturer("S Helper Service", 23);
       AddManufacturer("SLOMO Railroad Models", 142);
       AddManufacturer("SPROG DCC", 44);
       AddManufacturer("Sanda Kan Industrial", 95);
       AddManufacturer("Shourt Line", 90);
       AddManufacturer("Signaling Solution", 119);
       AddManufacturer("Silicon Railway", 33);
       AddManufacturer("SoundTraxx (Throttle-Up)", 141);
       AddManufacturer("Spectrum Engineering", 80);
       AddManufacturer("T4T - Technology for Trains GmbH", 20);
       AddManufacturer("TCH Technology", 54);
       AddManufacturer("Tam Valley Depot", 59);
       AddManufacturer("Tams Elektronik GmbH", 62);
       AddManufacturer("Tawcrafts", 92);
       AddManufacturer("Team Digital, LLC", 25);
       AddManufacturer("Tehnologistic (train-O-matic)", 78);
       AddManufacturer("The Electric Railroad Company", 73);
       AddManufacturer("Throttle-Up (SoundTraxx)", 141);
       AddManufacturer("Train Control Systems", 153);
       AddManufacturer("Train ID Systems", 138);
       AddManufacturer("Train Technology", 2);
       AddManufacturer("TrainModules", 61);
       AddManufacturer("TrainTech", 104);
       AddManufacturer("Trenes Digitales", 100);
       AddManufacturer("Trix Modelleisenbahn", 131);
       AddManufacturer("Uhlenbrock Elektronik", 85);
       AddManufacturer("Umelec Ing Buero", 147);
       AddManufacturer("Viessmann Modellspielwaren GmbH", 109);
       AddManufacturer("W.S. Ataras Engineering", 119);
       AddManufacturer("WP Railshops", 163);
       AddManufacturer("Wangrow", 12);
       AddManufacturer("Wekomm Engineering, GmbH", 86);
       AddManufacturer("Wm. K. Walthers, Inc", 150);
       AddManufacturer("ZTC", 132);
       AddManufacturer("Zimo", 145);
       AddManufacturer("csikos-muhely", 120);
       AddManufacturer("drM", 164);
    }
}