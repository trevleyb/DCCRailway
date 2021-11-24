using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DccTrainCmd.JMRI.Rosters {

	[XmlRoot (ElementName = "dcclocoaddress")]
	public class Dcclocoaddress {
		[XmlAttribute (AttributeName = "longaddress")]
		public string Longaddress { get; set; }

		[XmlAttribute (AttributeName = "number")]
		public string Number { get; set; }
	}

	[XmlRoot (ElementName = "decoder")]
	public class Decoder {
		[XmlAttribute (AttributeName = "comment")]
		public string Comment { get; set; }

		[XmlAttribute (AttributeName = "family")]
		public string Family { get; set; }

		[XmlAttribute (AttributeName = "maxFnNum")]
		public string MaxFnNum { get; set; }

		[XmlAttribute (AttributeName = "model")]
		public string Model { get; set; }
	}

	[XmlRoot (ElementName = "locoaddress")]
	public class Locoaddress {
		[XmlElement (ElementName = "dcclocoaddress")]
		public Dcclocoaddress Dcclocoaddress { get; set; }

		[XmlElement (ElementName = "number")]
		public string Number { get; set; }

		[XmlElement (ElementName = "protocol")]
		public string Protocol { get; set; }
	}

	[XmlRoot (ElementName = "locomotive")]
	public class Locomotive {
		[XmlAttribute (AttributeName = "comment")]
		public string Comment { get; set; }

		[XmlElement (ElementName = "dateUpdated")]
		public string DateUpdated { get; set; }

		[XmlAttribute (AttributeName = "dccAddress")]
		public string DccAddress { get; set; }

		[XmlElement (ElementName = "decoder")]
		public Decoder Decoder { get; set; }

		[XmlAttribute (AttributeName = "developerID")]
		public string DeveloperID { get; set; }

		[XmlAttribute (AttributeName = "fileName")]
		public string FileName { get; set; }

		[XmlElement (ElementName = "functionlabels")]
		public string Functionlabels { get; set; }

		[XmlAttribute (AttributeName = "iconFilePath")]
		public string IconFilePath { get; set; }

		[XmlAttribute (AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute (AttributeName = "imageFilePath")]
		public string ImageFilePath { get; set; }

		[XmlAttribute (AttributeName = "IsShuntingOn")]
		public string IsShuntingOn { get; set; }

		[XmlElement (ElementName = "locoaddress")]
		public Locoaddress Locoaddress { get; set; }

		[XmlAttribute (AttributeName = "manufacturerID")]
		public string ManufacturerID { get; set; }

		[XmlAttribute (AttributeName = "maxSpeed")]
		public string MaxSpeed { get; set; }

		[XmlAttribute (AttributeName = "mfg")]
		public string Mfg { get; set; }

		[XmlAttribute (AttributeName = "model")]
		public string Model { get; set; }

		[XmlAttribute (AttributeName = "owner")]
		public string Owner { get; set; }

		[XmlAttribute (AttributeName = "productID")]
		public string ProductID { get; set; }

		[XmlAttribute (AttributeName = "roadName")]
		public string RoadName { get; set; }

		[XmlAttribute (AttributeName = "roadNumber")]
		public string RoadNumber { get; set; }

		[XmlElement (ElementName = "soundlabels")]
		public string Soundlabels { get; set; }

		[XmlAttribute (AttributeName = "URL")]
		public string URL { get; set; }
	}

	[XmlRoot (ElementName = "roster")]
	public class JMRIRoster {
		[XmlElement (ElementName = "locomotive")]
		public Locomotive Locomotive { get; set; }
	}

	[XmlRoot (ElementName = "roster-config")]
	public class Rosterconfig {
		[XmlAttribute (AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
		public string NoNamespaceSchemaLocation { get; set; }

		[XmlElement (ElementName = "roster")]
		public JMRIRoster Roster { get; set; }

		[XmlAttribute (AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
	}

}
