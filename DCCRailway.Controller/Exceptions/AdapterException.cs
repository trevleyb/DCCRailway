using DCCRailway.Controller.Adapters.Base;

namespace DCCRailway.Controller.Exceptions;

public class AdapterException : Exception {
    public AdapterException(string? adapter, string? message, Exception? ex = null) : base(adapter + ":" + message, ex) => Adapter = adapter;
    public AdapterException(IAdapter adapter, string? message, Exception? ex = null) : base(adapter + ":" + message, ex) => Adapter = adapter.ToString();
    public string? Adapter { get; }
}