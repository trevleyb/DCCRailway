using System.IO.Ports;
using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using Serilog;

namespace DCCRailway.Controller.NCE.Adapters;

[Adapter("NCE Serial Adapter", AdapterType.Serial)]
public class NCESerial(ILogger logger) : SerialAdapter(logger), IAdapter {
    public NCESerial(ILogger logger, string portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : this(logger) {
        PortName = portName;
        BaudRate = baudRate;
        DataBits = dataBits;
        Parity   = parity;
        StopBits = stopBits;
        Timeout  = timeout;
    }
}