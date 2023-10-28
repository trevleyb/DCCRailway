using System.IO.Ports;
using DCCRailway.System.Exceptions;

namespace DCCRailway.System.Adapters;

public class SerialAdapterSettings {
    public string PortName { get; init; }
    public int Timeout { get; init; }
    public int BaudRate { get; init; }
    public int DataBits { get; init; }
    public Parity Parity { get; init; }
    public StopBits StopBits { get; init; }

    public SerialAdapterSettings(string portName, int baudRate = 19200, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.None, int timeout = 500) {
        PortName = portName ?? throw new ApplicationException("The Port must be specified.");
        BaudRate = baudRate;
        DataBits = dataBits;
        Parity = parity;
        StopBits = stopBits;
        Timeout = timeout;
    }
}