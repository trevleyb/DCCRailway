using System.IO.Ports;

namespace DCCRailway.Station.Adapters.Helpers;

public class SerialAdapterSettings(string portName, int baudRate = 19200, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.None, int timeout = 500) {
    public string   PortName { get; init; } = portName ?? throw new ApplicationException("The Port must be specified.");
    public int      Timeout  { get; init; } = timeout;
    public int      BaudRate { get; init; } = baudRate;
    public int      DataBits { get; init; } = dataBits;
    public Parity   Parity   { get; init; } = parity;
    public StopBits StopBits { get; init; } = stopBits;
}