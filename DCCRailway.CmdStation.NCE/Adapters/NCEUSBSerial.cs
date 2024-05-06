using System.IO.Ports;
using DCCRailway.CmdStation.Adapters;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.NCE.Adapters;

[Adapter("NCE USB Serial Adapter", AdapterType.USB)]
public class NCEUSBSerial : SerialAdapter, IAdapter {
    public NCEUSBSerial(string portName = "dev/ttyUSB0",
                        int baudRate = 9600,
                        int dataBits = 8,
                        Parity parity = Parity.None,
                        StopBits stopBits = StopBits.One,
                        int timeout = 2000) : base(portName, baudRate, dataBits, parity, stopBits, timeout) { }
}