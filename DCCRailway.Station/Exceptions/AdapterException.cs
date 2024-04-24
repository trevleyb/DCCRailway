using DCCRailway.Station.Adapters.Base;

namespace DCCRailway.Station.Exceptions;

public class AdapterException : Exception {
    public AdapterException(string? adapter, string? message, Exception? ex = null) : base(adapter + ":" + message, ex) => Adapter = adapter;
    public AdapterException(IAdapter adapter, string? message, Exception? ex = null) : base(adapter + ":" + message, ex) => Adapter = adapter.ToString();
    public string? Adapter { get; }
}