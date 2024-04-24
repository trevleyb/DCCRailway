using System.IO.Ports;
using DCCRailway.Station.Attributes;

namespace DCCRailway.Station.Adapters.Helpers;

public class SerialAdapterSettings(string portName, int baudRate = 19200, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.None, int timeout = 500) {
    [ParameterMappable] public string   PortName { get; init; } = portName ?? throw new ApplicationException("The Port must be specified.");
    [ParameterMappable] public int      Timeout  { get; init; } = timeout;
    [ParameterMappable] public int      BaudRate { get; init; } = baudRate;
    [ParameterMappable] public int      DataBits { get; init; } = dataBits;
    [ParameterMappable] public Parity   Parity   { get; init; } = parity;
    [ParameterMappable] public StopBits StopBits { get; init; } = stopBits;
}