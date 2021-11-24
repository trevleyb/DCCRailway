using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DCCRailway.Core.Config {
	[XmlRoot(ElementName = "NMRA_Manufacturers")]
	public class Manufacturers : List<Manufacturer> {
		public Manufacturers() {
			BuildDefaults();
		}

		public Manufacturer Find(string name) {
			foreach (var manufacturer in this) {
				if (manufacturer.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) return manufacturer;
			}
			return new Manufacturer { Name = "Unknown", Identifier = 0 };
		}

		public Manufacturer Find(byte id) {
			foreach (var manufacturer in this) {
				if (manufacturer.Identifier.Equals(id)) return manufacturer;
			}
			return new Manufacturer { Name = "Unknown", Identifier = 0 };
		}

		public void BuildDefaults() {
			// Copied from DecoderPro definitions which are from the MNRA Manufacturers List
			Add(new Manufacturer { Name = "Unknown", Identifier = 0 });
			Add(new Manufacturer { Name = "A-Train Electronics", Identifier = 137 });
			Add(new Manufacturer { Name = "AMW", Identifier = 19 });
			Add(new Manufacturer { Name = "ANE Model Co, Ltd", Identifier = 45 });
			Add(new Manufacturer { Name = "AXJ Electronics", Identifier = 110 });
			Add(new Manufacturer { Name = "Advance IC Engineering, Inc.", Identifier = 17 });
			Add(new Manufacturer { Name = "Aristo-Craft Trains", Identifier = 34 });
			Add(new Manufacturer { Name = "Arnold - Rivarossi", Identifier = 173 });
			Add(new Manufacturer { Name = "Atlas", Identifier = 127 });
			Add(new Manufacturer { Name = "AuroTrains", Identifier = 170 });
			Add(new Manufacturer { Name = "Auvidel", Identifier = 76 });
			Add(new Manufacturer { Name = "BLOCKsignalling", Identifier = 148 });
			Add(new Manufacturer { Name = "BRAWA Modellspielwaren GmbH", Identifier = 186 });
			Add(new Manufacturer { Name = "Bachmann Trains", Identifier = 101 });
			Add(new Manufacturer { Name = "Benezan Electronics", Identifier = 114 });
			Add(new Manufacturer { Name = "Berros", Identifier = 122 });
			Add(new Manufacturer { Name = "Blue Digital", Identifier = 225 });
			Add(new Manufacturer { Name = "Bluecher-Electronic", Identifier = 60 });
			Add(new Manufacturer { Name = "Brelec", Identifier = 31 });
			Add(new Manufacturer { Name = "Broadway Limited Imports, LLC", Identifier = 38 });
			Add(new Manufacturer { Name = "CML Systems", Identifier = 1 });
			Add(new Manufacturer { Name = "CMos Engineering", Identifier = 130 });
			Add(new Manufacturer { Name = "CT Elektronik", Identifier = 117 });
			Add(new Manufacturer { Name = "CVP Products", Identifier = 135 });
			Add(new Manufacturer { Name = "Capecom", Identifier = 47 });
			Add(new Manufacturer { Name = "Computer Dialysis France", Identifier = 105 });
			Add(new Manufacturer { Name = "Con-Com GmbH", Identifier = 204 });
			Add(new Manufacturer { Name = "DCC Supplies, Ltd", Identifier = 51 });
			Add(new Manufacturer { Name = "DCC Train Automation", Identifier = 144 });
			Add(new Manufacturer { Name = "DCC-Gaspar-Electronic", Identifier = 124 });
			Add(new Manufacturer { Name = "DCCconcepts", Identifier = 36 });
			Add(new Manufacturer { Name = "Dapol Limited", Identifier = 154 });
			Add(new Manufacturer { Name = "Desktop Station", Identifier = 140 });
			Add(new Manufacturer { Name = "Dietz Modellbahntechnik", Identifier = 115 });
			Add(new Manufacturer { Name = "Digi-CZ", Identifier = 152 });
			Add(new Manufacturer { Name = "Digikeijs", Identifier = 42 });
			Add(new Manufacturer { Name = "Digirails", Identifier = 42 });
			Add(new Manufacturer { Name = "Digital Bahn", Identifier = 64 });
			Add(new Manufacturer { Name = "Digitools Elektronika, Kft", Identifier = 75 });
			Add(new Manufacturer { Name = "Digitrax", Identifier = 129 });
			Add(new Manufacturer { Name = "Digsight", Identifier = 30 });
			Add(new Manufacturer { Name = "Doehler und Haass", Identifier = 97 });
			Add(new Manufacturer { Name = "E-Modell", Identifier = 69 });
			Add(new Manufacturer { Name = "ECCO GmbH", Identifier = 121 });
			Add(new Manufacturer { Name = "Educational Computer, Inc.", Identifier = 39 });
			Add(new Manufacturer { Name = "Electronic Solutions Ulm GmbH", Identifier = 151 });
			Add(new Manufacturer { Name = "Electronik and Model Produktion", Identifier = 35 });
			Add(new Manufacturer { Name = "Electroniscript, inc", Identifier = 94 });
			Add(new Manufacturer { Name = "Fleischmann", Identifier = 155 });
			Add(new Manufacturer { Name = "Frateschi Model Trains", Identifier = 128 });
			Add(new Manufacturer { Name = "Fucik", Identifier = 158 });
			Add(new Manufacturer { Name = "GFB Designs", Identifier = 46 });
			Add(new Manufacturer { Name = "Gaugemaster", Identifier = 65 });
			Add(new Manufacturer { Name = "GooVerModels", Identifier = 81 });
			Add(new Manufacturer { Name = "HAG Modelleisenbahn AG", Identifier = 82 });
			Add(new Manufacturer { Name = "HONS Model", Identifier = 88 });
			Add(new Manufacturer { Name = "Haber and Koenig Electronics GmbH", Identifier = 111 });
			Add(new Manufacturer { Name = "Harman DCC", Identifier = 98 });
			Add(new Manufacturer { Name = "Hattons Model Railways", Identifier = 79 });
			Add(new Manufacturer { Name = "Heljan A/S", Identifier = 28 });
			Add(new Manufacturer { Name = "Heller Modelbahn", Identifier = 67 });
			Add(new Manufacturer { Name = "Hornby", Identifier = 48 });
			Add(new Manufacturer { Name = "Integrated Signal Systems", Identifier = 102 });
			Add(new Manufacturer { Name = "Intelligent Command Control", Identifier = 133 });
			Add(new Manufacturer { Name = "JMRI", Identifier = 18 });
			Add(new Manufacturer { Name = "JSS-Elektronic", Identifier = 83 });
			Add(new Manufacturer { Name = "Joka Electronic", Identifier = 49 });
			Add(new Manufacturer { Name = "KAM Industries", Identifier = 22 });
			Add(new Manufacturer { Name = "KATO Precision Models", Identifier = 40 });
			Add(new Manufacturer { Name = "KRES GmbH", Identifier = 58 });
			Add(new Manufacturer { Name = "Kevtronics cc", Identifier = 93 });
			Add(new Manufacturer { Name = "Kreischer Datatechnik", Identifier = 21 });
			Add(new Manufacturer { Name = "Krois-Modell", Identifier = 52 });
			Add(new Manufacturer { Name = "Kuehn Ing.", Identifier = 157 });
			Add(new Manufacturer { Name = "LDH", Identifier = 56 });
			Add(new Manufacturer { Name = "LGB", Identifier = 159 });
			Add(new Manufacturer { Name = "LS Models Sprl", Identifier = 77 });
			Add(new Manufacturer { Name = "LSdigital", Identifier = 112 });
			Add(new Manufacturer { Name = "Lahti Associates", Identifier = 14 });
			Add(new Manufacturer { Name = "LaisDCC", Identifier = 134 });
			Add(new Manufacturer { Name = "Lenz", Identifier = 99 });
			Add(new Manufacturer { Name = "MAWE Elektronik", Identifier = 68 });
			Add(new Manufacturer { Name = "MBTronik - PiN GITmBH", Identifier = 26 });
			Add(new Manufacturer { Name = "MD Electronics", Identifier = 160 });
			Add(new Manufacturer { Name = "MERG", Identifier = 165 });
			Add(new Manufacturer { Name = "MRC", Identifier = 143 });
			Add(new Manufacturer { Name = "MTB Model", Identifier = 72 });
			Add(new Manufacturer { Name = "MTH Electric Trains, Inc.", Identifier = 27 });
			Add(new Manufacturer { Name = "Maison de DCC", Identifier = 166 });
			Add(new Manufacturer { Name = "Massoth Elektronik, GmbH", Identifier = 123 });
			Add(new Manufacturer { Name = "Mistral Train Models", Identifier = 29 });
			Add(new Manufacturer { Name = "MoBaTrain.de", Identifier = 24 });
			Add(new Manufacturer { Name = "MyLocoSound", Identifier = 116 });
			Add(new Manufacturer { Name = "MÜT GmbH", Identifier = 118 });
			Add(new Manufacturer { Name = "Möllehem Gårdsproduktion", Identifier = 126 });
			Add(new Manufacturer { Name = "N&amp;Q Electronics", Identifier = 50 });
			Add(new Manufacturer { Name = "NAC Services, Inc", Identifier = 37 });
			Add(new Manufacturer { Name = "NMRA Reserved", Identifier = 238 });
			Add(new Manufacturer { Name = "NYRS", Identifier = 136 });
			Add(new Manufacturer { Name = "Nagasue System Design Office", Identifier = 103 });
			Add(new Manufacturer { Name = "Nagoden", Identifier = 108 });
			Add(new Manufacturer { Name = "New York Byano Limited", Identifier = 71 });
			Add(new Manufacturer { Name = "Ngineering", Identifier = 43 });
			Add(new Manufacturer { Name = "Noarail", Identifier = 63 });
			Add(new Manufacturer { Name = "North Coast Engineering", Identifier = 11 });
			Add(new Manufacturer { Name = "Nucky", Identifier = 156 });
			Add(new Manufacturer { Name = "Opherline1", Identifier = 106 });
			Add(new Manufacturer { Name = "PIKO", Identifier = 162 });
			Add(new Manufacturer { Name = "PRICOM Design", Identifier = 96 });
			Add(new Manufacturer { Name = "PSI-Dynatrol", Identifier = 14 });
			Add(new Manufacturer { Name = "Passmann", Identifier = 41 });
			Add(new Manufacturer { Name = "Phoenix Sound Systems, Inc.", Identifier = 107 });
			Add(new Manufacturer { Name = "Pojezdy.EU", Identifier = 89 });
			Add(new Manufacturer { Name = "PpP Digital", Identifier = 74 });
			Add(new Manufacturer { Name = "Praecipuus", Identifier = 33 });
			Add(new Manufacturer { Name = "ProfiLok Modellbahntechnik GmbH", Identifier = 125 });
			Add(new Manufacturer { Name = "Public-domain and DIY", Identifier = 13 });
			Add(new Manufacturer { Name = "QElectronics GmbH", Identifier = 55 });
			Add(new Manufacturer { Name = "QSIndustries", Identifier = 113 });
			Add(new Manufacturer { Name = "RR-CirKits", Identifier = 87 });
			Add(new Manufacturer { Name = "Railflyer Model Prototypes, Inc.", Identifier = 84 });
			Add(new Manufacturer { Name = "Railnet Solutions, LLC", Identifier = 66 });
			Add(new Manufacturer { Name = "Rails Europ Express", Identifier = 146 });
			Add(new Manufacturer { Name = "Railstars Limited", Identifier = 91 });
			Add(new Manufacturer { Name = "RamFixx Technologies (Wangrow)", Identifier = 15 });
			Add(new Manufacturer { Name = "Rampino Elektronik", Identifier = 57 });
			Add(new Manufacturer { Name = "Rautenhaus Digital Vertrieb", Identifier = 53 });
			Add(new Manufacturer { Name = "RealRail Effects", Identifier = 139 });
			Add(new Manufacturer { Name = "Regal Way Co. Ltd", Identifier = 32 });
			Add(new Manufacturer { Name = "Rock Junction Controls", Identifier = 149 });
			Add(new Manufacturer { Name = "Roco Modelspielwaren", Identifier = 161 });
			Add(new Manufacturer { Name = "Rocrail", Identifier = 70 });
			Add(new Manufacturer { Name = "S Helper Service", Identifier = 23 });
			Add(new Manufacturer { Name = "SLOMO Railroad Models", Identifier = 142 });
			Add(new Manufacturer { Name = "SPROG DCC", Identifier = 44 });
			Add(new Manufacturer { Name = "Sanda Kan Industrial", Identifier = 95 });
			Add(new Manufacturer { Name = "Shourt Line", Identifier = 90 });
			Add(new Manufacturer { Name = "Signaling Solution", Identifier = 119 });
			Add(new Manufacturer { Name = "Silicon Railway", Identifier = 33 });
			Add(new Manufacturer { Name = "SoundTraxx (Throttle-Up)", Identifier = 141 });
			Add(new Manufacturer { Name = "Spectrum Engineering", Identifier = 80 });
			Add(new Manufacturer { Name = "T4T - Technology for Trains GmbH", Identifier = 20 });
			Add(new Manufacturer { Name = "TCH Technology", Identifier = 54 });
			Add(new Manufacturer { Name = "Tam Valley Depot", Identifier = 59 });
			Add(new Manufacturer { Name = "Tams Elektronik GmbH", Identifier = 62 });
			Add(new Manufacturer { Name = "Tawcrafts", Identifier = 92 });
			Add(new Manufacturer { Name = "Team Digital, LLC", Identifier = 25 });
			Add(new Manufacturer { Name = "Tehnologistic (train-O-matic)", Identifier = 78 });
			Add(new Manufacturer { Name = "The Electric Railroad Company", Identifier = 73 });
			Add(new Manufacturer { Name = "Throttle-Up (SoundTraxx)", Identifier = 141 });
			Add(new Manufacturer { Name = "Train Control Systems", Identifier = 153 });
			Add(new Manufacturer { Name = "Train ID Systems", Identifier = 138 });
			Add(new Manufacturer { Name = "Train Technology", Identifier = 2 });
			Add(new Manufacturer { Name = "TrainModules", Identifier = 61 });
			Add(new Manufacturer { Name = "TrainTech", Identifier = 104 });
			Add(new Manufacturer { Name = "Trenes Digitales", Identifier = 100 });
			Add(new Manufacturer { Name = "Trix Modelleisenbahn", Identifier = 131 });
			Add(new Manufacturer { Name = "Uhlenbrock Elektronik", Identifier = 85 });
			Add(new Manufacturer { Name = "Umelec Ing Buero", Identifier = 147 });
			Add(new Manufacturer { Name = "Viessmann Modellspielwaren GmbH", Identifier = 109 });
			Add(new Manufacturer { Name = "W.S. Ataras Engineering", Identifier = 119 });
			Add(new Manufacturer { Name = "WP Railshops", Identifier = 163 });
			Add(new Manufacturer { Name = "Wangrow", Identifier = 12 });
			Add(new Manufacturer { Name = "Wekomm Engineering, GmbH", Identifier = 86 });
			Add(new Manufacturer { Name = "Wm. K. Walthers, Inc", Identifier = 150 });
			Add(new Manufacturer { Name = "ZTC", Identifier = 132 });
			Add(new Manufacturer { Name = "Zimo", Identifier = 145 });
			Add(new Manufacturer { Name = "csikos-muhely", Identifier = 120 });
			Add(new Manufacturer { Name = "drM", Identifier = 164 });
		}
	}
}