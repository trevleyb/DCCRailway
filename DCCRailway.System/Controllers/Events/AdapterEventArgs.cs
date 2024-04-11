using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Events;

namespace DCCRailway.System.Controllers.Events;

public enum AdapterEventType {Attach, Detatch, DataSent, DataRecv, Error}

public class AdapterEventArgs(IAdapter adapter, AdapterEventType adapterEvent, byte[]? adapterData = null, string message = "") : ControllerEventArgs(message) {
    public IAdapter         Adapter      { get; set; } = adapter;
    public AdapterEventType AdapterEvent { get; set; } = adapterEvent;
    public byte[]?          Data         { get; set; } = adapterData;
}