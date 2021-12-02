using System.IO.Ports;
using DCCRailway.Core.Systems.Adapters;

namespace DCCRailway.Systems.NCE.Adapters {
	public class NCEUSBSerial : SerialAdapter, IAdapter {
		public NCEUSBSerial(string portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : base(portName, baudRate, dataBits, parity, stopBits, timeout) { }

		public new static string Name {
			get { return "NCE USB Serial Interface"; }
		}

		public override string Description {
			get { return "NCE-USBSerial"; }
		}
	}
}