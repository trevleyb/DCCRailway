using System.IO.Ports;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Attributes;

namespace DCCRailway.Systems.NCE.Adapters; 

[Adapter("NCE Serial Adapter", AdapterType.Serial)]
public class NCESerial : SerialAdapter, IAdapter {
    public NCESerial(string portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : base(portName, baudRate, dataBits, parity, stopBits, timeout) { }
}