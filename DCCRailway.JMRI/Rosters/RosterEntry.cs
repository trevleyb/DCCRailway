// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(LocomotiveConfig));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (LocomotiveConfig)serializer.Deserialize(reader);
// }

/*
namespace DccTrainCmd.JMRI.Rosters {

	[XmlRoot (ElementName = "decoder")]
	public class Decoder {

		[XmlAttribute (AttributeName = "model")]
		public string Model;

		[XmlAttribute (AttributeName = "family")]
		public DateTime Family;

		[XmlAttribute (AttributeName = "comment")]
		public string Comment;

		[XmlAttribute (AttributeName = "maxFnNum")]
		public int MaxFnNum;
	}

	[XmlRoot (ElementName = "dcclocoaddress")]
	public class Dcclocoaddress {

		[XmlAttribute (AttributeName = "number")]
		public int Number;

		[XmlAttribute (AttributeName = "longaddress")]
		public string Longaddress;
	}

	[XmlRoot (ElementName = "locoaddress")]
	public class Locoaddress {

		[XmlElement (ElementName = "dcclocoaddress")]
		public Dcclocoaddress Dcclocoaddress;

		[XmlElement (ElementName = "number")]
		public int Number;

		[XmlElement (ElementName = "protocol")]
		public string Protocol;
	}

	[XmlRoot (ElementName = "varValue")]
	public class VarValue {

		[XmlAttribute (AttributeName = "item")]
		public string Item;

		[XmlAttribute (AttributeName = "value")]
		public int Value;
	}

	[XmlRoot (ElementName = "decoderDef")]
	public class DecoderDef {

		[XmlElement (ElementName = "varValue")]
		public List<VarValue> VarValue;
	}

	[XmlRoot (ElementName = "CVvalue")]
	public class CVvalue {

		[XmlAttribute (AttributeName = "name")]
		public int Name;

		[XmlAttribute (AttributeName = "value")]
		public int Value;
	}

	[XmlRoot (ElementName = "values")]
	public class Values {

		[XmlElement (ElementName = "decoderDef")]
		public DecoderDef DecoderDef;

		[XmlElement (ElementName = "CVvalue")]
		public List<CVvalue> CVvalue;
	}

	[XmlRoot (ElementName = "locomotive")]
	public class Locomotive {

		[XmlElement (ElementName = "dateUpdated")]
		public DateTime DateUpdated;

		[XmlElement (ElementName = "decoder")]
		public Decoder Decoder;

		[XmlElement (ElementName = "locoaddress")]
		public Locoaddress Locoaddress;

		[XmlElement (ElementName = "functionlabels")]
		public object Functionlabels;

		[XmlElement (ElementName = "soundlabels")]
		public object Soundlabels;

		[XmlElement (ElementName = "values")]
		public Values Values;

		[XmlAttribute (AttributeName = "id")]
		public int Id;

		[XmlAttribute (AttributeName = "fileName")]
		public string FileName;

		[XmlAttribute (AttributeName = "roadNumber")]
		public int RoadNumber;

		[XmlAttribute (AttributeName = "roadName")]
		public string RoadName;

		[XmlAttribute (AttributeName = "mfg")]
		public string Mfg;

		[XmlAttribute (AttributeName = "owner")]
		public string Owner;

		[XmlAttribute (AttributeName = "model")]
		public string Model;

		[XmlAttribute (AttributeName = "dccAddress")]
		public int DccAddress;

		[XmlAttribute (AttributeName = "comment")]
		public string Comment;

		[XmlAttribute (AttributeName = "developerID")]
		public int DeveloperID;

		[XmlAttribute (AttributeName = "manufacturerID")]
		public int ManufacturerID;

		[XmlAttribute (AttributeName = "productID")]
		public int ProductID;

		[XmlAttribute (AttributeName = "maxSpeed")]
		public int MaxSpeed;

		[XmlAttribute (AttributeName = "imageFilePath")]
		public object ImageFilePath;

		[XmlAttribute (AttributeName = "iconFilePath")]
		public object IconFilePath;

		[XmlAttribute (AttributeName = "URL")]
		public object URL;

		[XmlAttribute (AttributeName = "IsShuntingOn")]
		public object IsShuntingOn;

		[XmlText]
		public string Text;
	}

	[XmlRoot (ElementName = "locomotive-config")]
	public class Locomotiveconfig {

		[XmlElement (ElementName = "locomotive")]
		public Locomotive Locomotive;

		[XmlAttribute (AttributeName = "xsi")]
		public string Xsi;

		[XmlAttribute (AttributeName = "noNamespaceSchemaLocation")]
		public string NoNamespaceSchemaLocation;

		[XmlText]
		public string Text;
	}
}

*/
